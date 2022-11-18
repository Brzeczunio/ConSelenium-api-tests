using ConSelenium.Api.Client.Builders;
using ConSelenium.Common;
using ConSelenium.Settings.Models;
using RestSharp;
using System.Security.Authentication;

namespace ConSelenium.Settings.Base
{
    public abstract class BaseContext
    {
        protected TokenInfo TokenInfo { get; set; }

        public async void Login(string userName, string password)
        {
            var client = new LoggerDecoratedRestClient(TestConfiguration.Settings.BaseUri);
            var request = new RestRequest("api/v1/account/login");
            request.AddJsonBody(new UserLoginBuilder().AddUserName(userName).AddPassword(password).Build());

            var response = await client.ExecutePostAsync<TokenInfo>(request);

            if (response.Data == null)
            {
                throw new AuthenticationException("User wasn't logged");
            }

            SetAuth(response.Data);
        }

        public abstract void SetAuth(TokenInfo tokenInfo);
    }
}
