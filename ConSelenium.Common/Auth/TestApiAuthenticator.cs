using ConSelenium.Api.Client.Builders;
using ConSelenium.Settings.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Security.Authentication;

namespace ConSelenium.Common.Auth
{
    public class TestApiAuthenticator : AuthenticatorBase
    {
        readonly Uri _baseUrl;
        readonly string _userName, _password;

        public TestApiAuthenticator(Uri baseUrl, string userName, string password) : base("")
        {
            _baseUrl = baseUrl;
            _userName = userName;
            _password = password;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, Token);
        }

        async Task<string> GetToken()
        {
            var client = new LoggerDecoratedRestClient(_baseUrl);
            var request = new RestRequest("api/v1/account/login");
            request.AddJsonBody(new UserLoginBuilder().AddUserName(_userName).AddPassword(_password).Build());

            var response = await client.ExecutePostAsync<TokenInfo>(request);
            if (response.Data == null)
            {
                throw new AuthenticationException("User wasn't logged");
            }
            return $"bearer {response.Data.AccessToken}";
        }
    }
}
