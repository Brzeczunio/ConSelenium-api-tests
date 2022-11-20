using ConSelenium.Api.Client.Models.Responses;
using ConSelenium.Common.Interfaces;
using ConSelenium.Common.Enums;
using RestSharp;
using ConSelenium.Api.Client.Models.Requests;
using ConSelenium.Common.Tools;
using System.Net;

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

        public async Task<RestResponse<Created>> CreateProductResponse(ProductRequest product)
        {
            var request = new RestRequest($"api/{_apiVersion}/products");
            request.AddJsonBody(product);

            return await _restClient.ExecutePostAsync<Created>(request);
        }

        public async Task<Created> CreateProduct(ProductRequest product)
        {
            var response = await CreateProductResponse(product);

            return CheckData(response);
        }

        public async Task<RestResponse<IEnumerable<Product>>> GetProductsResponse()
        {
            var request = new RestRequest($"api/{_apiVersion}/products");

            return await _restClient.ExecuteGetAsync<IEnumerable<Product>>(request);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var response = await GetProductsResponse();

            return CheckData(response);
        }

        public async Task<RestResponse<Product>> GetProductResponse(int productId)
        {
            var request = new RestRequest($"api/{_apiVersion}/products/{productId}");

            return await _restClient.ExecuteGetAsync<Product>(request);
        }

        public async Task<Product> GetProduct(int productId)
        {
            var response = await GetProductResponse(productId);

            return CheckData(response);
        }

        public async Task<RestResponse<Created>> CreateOrderResponse(int userId, OrderRequest order)
        {
            var request = new RestRequest($"api/{_apiVersion}/users/{userId}/orders");
            request.AddJsonBody(order);

            return await _restClient.ExecutePostAsync<Created>(request);
        }

        public async Task<Created> CreateOrder(int userId, OrderRequest order)
        {
            var response = await CreateOrderResponse(userId, order);

            return CheckData(response);
        }

        public async Task<RestResponse<IEnumerable<Order>>> GetOrdersResponse(int userId)
        {
            var request = new RestRequest($"api/{_apiVersion}/users/{userId}/orders");

            return await _restClient.ExecuteGetAsync<IEnumerable<Order>>(request);
        }

        public async Task<IEnumerable<Order>> GetOrders(int userId)
        {
            var response = await GetOrdersResponse(userId);

            return CheckData(response);
        }

        public async Task<RestResponse<Order>> GetOrderResponse(int userId, int orderId)
        {
            var request = new RestRequest($"api/{_apiVersion}/users/{userId}/orders/{orderId}");

            return await RetryPolicy.GetRestRequestResult(async () => await _restClient.ExecuteGetAsync<Order>(request), r => r.StatusCode == HttpStatusCode.OK);
        }

        public async Task<Order> GetOrder(int userId, int orderId)
        {
            var response = await GetOrderResponse(userId, orderId);

            return CheckData(response);
        }

        public async Task<RestResponse<Created>> CreateUserResponse(UserRequest user)
        {
            var request = new RestRequest($"api/{_apiVersion}/users");
            request.AddJsonBody(user);

            return await _restClient.ExecutePostAsync<Created>(request);
        }

        public async Task<Created> CreateUser(UserRequest user)
        {
            var response = await CreateUserResponse(user);

            return CheckData(response);
        }

        public async Task<RestResponse<User>> GetUserResponse(int userId)
        {
            var request = new RestRequest($"api/{_apiVersion}/users/{userId}");

            return await _restClient.ExecuteGetAsync<User>(request);
        }

        public async Task<User> GetUser(int userId)
        {
            var response = await GetUserResponse(userId);

            return CheckData(response);
        }

        private T CheckData<T>(RestResponse<T> response)
        {
            if (response.Data == null)
            {
                throw new Exception($"{response.ResponseUri} failed with status code: {response.StatusCode}\nError was: {response.ErrorMessage}");
            }

            return response.Data;
        }
    }
}