using RestSharp;
using RestSharp.Authenticators;

namespace ConSelenium.Common.Interfaces
{
    public interface IRestClient
    {
        IAuthenticator Authenticator { get; set; }
        RestResponse Execute(RestRequest request);
        Task<RestResponse> ExecuteAsync(RestRequest request);
        Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request);
        Task<RestResponse> ExecuteGetAsync(RestRequest request);
        Task<RestResponse<T>> ExecuteGetAsync<T>(RestRequest request);
        Task<RestResponse> ExecutePostAsync(RestRequest request);
        Task<RestResponse<T>> ExecutePostAsync<T>(RestRequest request);
        Task<RestResponse> ExecutePutAsync(RestRequest request);
        Task<RestResponse<T>> ExecutePutAsync<T>(RestRequest request);
    }
}
