using ConSelenium.Api.Client.Models;
using ConSelenium.Common.Interfaces;
using RestSharp;

namespace ConSelenium.Api.Client
{
    public class TestApiClient
    {
        private readonly IRestClient _restClient;

        public TestApiClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<RestResponse<IEnumerable<Product>>> GetProducts()
        {
            var request = new RestRequest("api/products");

            return await _restClient.ExecuteGetAsync<IEnumerable<Product>>(request);
        }
    }
}