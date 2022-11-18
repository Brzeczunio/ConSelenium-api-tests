using ConSelenium.Api.Client.Models.Responses;
using ConSelenium.Common.Interfaces;
using ConSelenium.Common.Enums;
using RestSharp;
using ConSelenium.Api.Client.Models.Requests;

namespace ConSelenium.Api.Client
{
    public class TestApiClient
    {
        private readonly IRestClient _restClient;
        private readonly ApiVersion _apiVersion;

        public TestApiClient(IRestClient restClient, ApiVersion version = ApiVersion.v1)
        {
            _restClient = restClient;
            _apiVersion = version;
        }

        public async Task<RestResponse<Created>> CreateProduct(ProductRequest product)
        {
            var request = new RestRequest($"api/{_apiVersion}/products");
            request.AddJsonBody(product);

            return await _restClient.ExecutePostAsync<Created>(request);
        }

        public async Task<RestResponse<IEnumerable<Product>>> GetProducts()
        {
            var request = new RestRequest($"api/{_apiVersion}/products");

            return await _restClient.ExecuteGetAsync<IEnumerable<Product>>(request);
        }

        public async Task<RestResponse<Product>> GetProduct(int productId)
        {
            var request = new RestRequest($"api/{_apiVersion}/products/{productId}");

            return await _restClient.ExecuteGetAsync<Product>(request);
        }

        public async Task<RestResponse<Created>> CreateOrder(int userId, OrderRequest order)
        {
            var request = new RestRequest($"api/{_apiVersion}/users/{userId}/orders");
            request.AddJsonBody(order);

            return await _restClient.ExecutePostAsync<Created>(request);
        }

        public async Task<RestResponse<IEnumerable<Order>>> GetOrders(int userId)
        {
            var request = new RestRequest($"api/{_apiVersion}/users/{userId}/orders");

            return await _restClient.ExecuteGetAsync<IEnumerable<Order>>(request);
        }

        public async Task<RestResponse<Order>> GetOrder(int userId, int orderId)
        {
            var request = new RestRequest($"api/{_apiVersion}/users/{userId}/orders/{orderId}");

            return await _restClient.ExecuteGetAsync<Order>(request);
        }

        public async Task<RestResponse<Created>> CreateUser(UserRequest user)
        {
            var request = new RestRequest($"api/{_apiVersion}/users");
            request.AddJsonBody(user);

            return await _restClient.ExecutePostAsync<Created>(request);
        }

        public async Task<RestResponse<User>> GetUser(int userId)
        {
            var request = new RestRequest($"api/{_apiVersion}/users/{userId}");

            return await _restClient.ExecuteGetAsync<User>(request);
        }
    }
}