using ConSelenium.Api.Client;
using ConSelenium.Api.Client.Builders;
using ConSelenium.Settings;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Allure.Core;

namespace ConSelenium.Api.Tests
{
    [AllureNUnit]
    public class Tests
    {
        [Test]
        [Parallelizable]
        public async Task WhenUserCreates_ShouldBeCreated()
        {
            //Arrange
            var client = new TestApiClient(new TestApiContext().TestApiClient);
            var random = new Random();
            var address = new AddressBuilder()
                .AddCity("Bytom")
                .AddhouseNumber("5a/2")
                .AddStreet("Włoska")
                .AddPostalcode("42-612")
                .Build();
            var userRequest = new UserRequestBuilder()
                .AddAddress(address)
                .AddEmail($"xzyz-{random.Next()}@xczx.pl")
                .AddPassword("prostehaslo")
                .AddSurname("Brzeczek")
                .AddName("Damian")
                .AddUserName($"brzeczunio-{random.Next()}")
                .Build();

            //Act
            var response = await client.CreateUser(userRequest);

            //Assert 
            userRequest.Password = null;
            var userResponse = await client.GetUser(response.Id);
            userResponse.Should().BeEquivalentTo(userRequest);
        }

        [Test]
        [Parallelizable]
        public async Task WhenAdminCreateProduct_ShouldBeCreated()
        {
            //Arrange
            var client = new TestApiClient(new TestApiContext().TestApiClient);
            var productRequest = new ProductRequestBuilder()
                .AddName("Sukienka")
                .AddDescription("Super sukienka")
                .AddPrice(200)
                .AddStock(300)
                .Build();

            //Act
            var response = await client.CreateProduct(productRequest);

            //Assert
            var productResponse = await client.GetProduct(response.Id);
            productResponse.Should().BeEquivalentTo(productRequest);
        }
        
        [Test]
        [Parallelizable]
        public async Task WhenUserCreateOrder_ShouldBeCreated()
        {
            //Arrange
            var client = new TestApiClient(new TestApiContext().TestApiClient);
            var random = new Random();
            var address = new AddressBuilder()
                .AddCity("Bytom")
                .AddhouseNumber("5a/2")
                .AddStreet("Włoska")
                .AddPostalcode("42-612")
                .Build();
            var userRequest = new UserRequestBuilder()
                .AddAddress(address)
                .AddEmail($"xzyz-{random.Next()}@xczx.pl")
                .AddPassword("prostehaslo")
                .AddSurname("Brzeczek")
                .AddName("Damian")
                .AddUserName($"brzeczunio-{random.Next()}")
                .Build();
            var stock = 100;
            var userResponse = await client.CreateUser(userRequest);
            var productRequest = new ProductRequestBuilder()
                .AddName("Spódniczka")
                .AddDescription("Balowa spódniczka")
                .AddPrice(100)
                .AddStock(stock)
                .Build();
            var productResponse = await client.CreateProduct(productRequest);
            var orderProduct = new OrderProductBuilder()
                .AddProductId(productResponse.Id)
                .AddQuantity((int)(stock * 0.1))
                .Build();
            var orderRequest = new OrderRequestBuilder()
                .AddOrderProduct(orderProduct)
                .Build();

            //Act
            var response = await client.CreateOrder(userResponse.Id, orderRequest);

            //Assert
            var orderResponse = await client.GetOrder(userResponse.Id, response.Id);
            using (new AssertionScope())
            {
                orderResponse.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(1));
                orderResponse.Should().BeEquivalentTo(orderRequest, o => o.ExcludingMissingMembers());
            };
        }
    }
}