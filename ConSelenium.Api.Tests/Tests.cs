using ConSelenium.Api.Client;
using ConSelenium.Api.Client.Builders;
using ConSelenium.Api.Client.Models.Requests;
using ConSelenium.Common.Tools;
using ConSelenium.Settings;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Net;

namespace ConSelenium.Api.Tests
{
    public class Tests
    {
        TestApiClient _client;
        UserRequest _userRequest;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _client = new TestApiClient(new TestApiContext().TestApiClient);
        }

        [SetUp]
        public void SetUp()
        {
            //Arrange
            var random = new Random();
            var address = new AddressBuilder()
                .AddCity("Bytom")
                .AddhouseNumber("5a/2")
                .AddStreet("Włoska")
                .AddPostalcode("42-612")
                .Build();
            _userRequest = new UserRequestBuilder()
                .AddAddress(address)
                .AddEmail($"xzyz-{random.Next()}@xczx.pl")
                .AddPassword("prostehaslo")
                .AddSurname("Brzeczek")
                .AddName("Damian")
                .AddUserName($"brzeczunio-{random.Next()}")
                .Build();
        }


        [Test]
        public async Task WhenUserCreates_ShouldBeCreated()
        {
            //Act
            var response = await _client.CreateUser(_userRequest);

            //Assert
            _userRequest.Password = null;
            var userResponse = await _client.GetUser(response.Data?.Id ?? 0);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                userResponse.Data.Should().BeEquivalentTo(_userRequest);
            };
        }

        [Test]
        public async Task WhenAdminCreateProduct_ShouldBeCreated()
        {
            //Arrange
            var productRequest = new ProductRequestBuilder()
                .AddName("Sukienka")
                .AddDescription("Super sukienka")
                .AddPrice(200)
                .AddStock(300)
                .Build();

            //Act
            var response = await _client.CreateProduct(productRequest);

            //Assert
            var productResponse = await _client.GetProduct(response.Data?.Id ?? 0);
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                productResponse.Data.Should().BeEquivalentTo(productRequest);
            };
        }

        [Test]
        public async Task WhenUserCreateOrder_ShouldBeCreated()
        {
            //Arrange
            var stock = 100;
            var userResponse = await _client.CreateUser(_userRequest);
            var productRequest = new ProductRequestBuilder()
                .AddName("Spódniczka")
                .AddDescription("Balowa spódniczka")
                .AddPrice(100)
                .AddStock(stock)
                .Build();
            var productResponse = await _client.CreateProduct(productRequest);
            var orderProduct = new OrderProductBuilder()
                .AddProductId(productResponse.Data?.Id ?? 0)
                .AddQuantity((int)(stock * 0.1))
                .Build();
            var orderRequest = new OrderRequestBuilder()
                .AddOrderProduct(orderProduct)
                .Build();

            //Act
            var response = await _client.CreateOrder(userResponse.Data?.Id ?? 0, orderRequest);

            //Assert
            var orderResponse = await RetryPolicy.GetRestRequestResult(async () => await _client.GetOrder(userResponse.Data?.Id ?? 0, response.Data?.Id ?? 0), r => r.StatusCode == HttpStatusCode.OK);
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                orderResponse.Data.CreationDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(30));
                orderResponse.Data.Should().BeEquivalentTo(orderRequest, o => o.ExcludingMissingMembers());
            };
        }
    }
}