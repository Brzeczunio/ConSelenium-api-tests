using ConSelenium.Api.Client;
using ConSelenium.Api.Client.Builders;
using ConSelenium.Api.Client.Models.Responses;
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
        public async Task WhenUserCreated_ShouldBeCreated()
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
            var userId = response.Id;
            userRequest.Id = userId;
            userRequest.Password = null;
            var userResponse = await client.GetUser(userId);
            userResponse.Should().BeEquivalentTo(userRequest);
        }

        [Test]
        [Parallelizable]
        public async Task WhenAdminCreatesProduct_ShouldBeCreated()
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
            var porductId = response.Id;
            productRequest.Id = porductId;
            var productResponse = await client.GetProduct(porductId);
            productResponse.Should().BeEquivalentTo(productRequest);
        }
        
        [Test]
        [Parallelizable]
        public async Task WhenUserCreatesOrder_ShouldBeCreated()
        {
            //Arrange
            var random = new Random();
            var userName = $"brzeczunio-{random.Next()}";
            var password = "prostehaslo";
            var client = new TestApiClient(new TestApiContext().TestApiClient);
            var address = new AddressBuilder()
                .AddCity("Bytom")
                .AddhouseNumber("5a/2")
                .AddStreet("Włoska")
                .AddPostalcode("42-612")
                .Build();
            var userRequest = new UserRequestBuilder()
                .AddAddress(address)
                .AddEmail($"xzyz-{random.Next()}@xczx.pl")
                .AddPassword(password)
                .AddSurname("Brzeczek")
                .AddName("Damian")
                .AddUserName(userName)
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
            var product = await client.GetProduct(productResponse.Id);
            var quantity = (int)(stock * 0.1);
            var expectedOrderProduct = new Order
            {
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        Product = product,
                        Quantity = quantity
                    }
                },
                CreationDate = DateTime.Now
            };
            var apiContext = new TestApiContext();
            apiContext.Login(userName, password);
            client = new TestApiClient(apiContext.TestApiClient);
            var orderProduct = new OrderProductBuilder()
                .AddProductId(productResponse.Id)
                .AddQuantity(quantity)
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
                orderResponse.OrderProducts.Select(x => x.Should().BeEquivalentTo(expectedOrderProduct.OrderProducts));
                orderResponse.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(1));
                orderResponse.CreationDate.Should().BeCloseTo(expectedOrderProduct.CreationDate, TimeSpan.FromHours(1));
            };
        }
    }
}