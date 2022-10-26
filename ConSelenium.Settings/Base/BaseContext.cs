using ConSelenium.Api.Client.Builders;
using ConSelenium.Common;
using RestSharp;

namespace ConSelenium.Settings.Base
{
    public abstract class BaseContext
    {
        public async void Login(string userName, string password)
        {
            var client = new LoggerDecoratedRestClient(TestConfiguration.Settings.BaseUri);

            var request = new RestRequest("api/v1/Login");

            request.AddJsonBody(new UserLoginBuilder().AddUserName(userName).AddPassword(password));

            await client.ExecuteGetAsync(request);
        }

        public virtual void Authenticate()
        {

        }
    }
}
