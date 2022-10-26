using ConSelenium.Common.Interfaces;
using RestSharp;
using RestSharp.Authenticators;

namespace ConSelenium.Common
{
    public class LoggerDecoratedRestClient : IRestClient
    {
        private RestClient _restClient { get; set; }

        public LoggerDecoratedRestClient(Uri baseUri)
        {
            var options = new RestClientOptions
            {
                BaseUrl = baseUri
            };

            _restClient = new RestClient(options);
        }

        public IAuthenticator Authenticator 
        {
            get => _restClient.Authenticator;
            set => _restClient.Authenticator = value; 
        }

        public RestResponse Execute(RestRequest request)
        {
            return _restClient.Execute(request);
        }

        public Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            return _restClient.ExecuteAsync(request);
        }

        public Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request)
        {
            return _restClient.ExecuteAsync<T>(request);
        }

        public Task<RestResponse> ExecuteGetAsync(RestRequest request)
        {
            return _restClient.ExecuteGetAsync(request);
        }

        public Task<RestResponse<T>> ExecuteGetAsync<T>(RestRequest request)
        {
            return _restClient.ExecuteGetAsync<T>(request);
        }

        public Task<RestResponse> ExecutePostAsync(RestRequest request)
        {
            return _restClient.ExecutePostAsync(request);
        }

        public Task<RestResponse<T>> ExecutePostAsync<T>(RestRequest request)
        {
            return _restClient.ExecutePostAsync<T>(request);
        }

        public Task<RestResponse> ExecutePutAsync(RestRequest request)
        {
            return _restClient.ExecutePutAsync(request);
        }

        public Task<RestResponse<T>> ExecutePutAsync<T>(RestRequest request)
        {
            return _restClient.ExecutePutAsync<T>(request);
        }
    }
}