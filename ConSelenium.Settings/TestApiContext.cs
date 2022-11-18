using ConSelenium.Common;
using ConSelenium.Common.Auth;
using ConSelenium.Settings.Enums;

namespace ConSelenium.Settings
{
    public class TestApiContext
    {
        public LoggerDecoratedRestClient TestApiClient { get; set; }

        public TestApiContext() : base()
        {
            TestApiClient = new LoggerDecoratedRestClient(TestConfiguration.Settings.BaseUri);
            TestApiClient.Authenticator = new TestApiAuthenticator(
                TestConfiguration.Settings.BaseUri, TestConfiguration.Settings.Users[User.Admin].Login, TestConfiguration.Settings.Users[User.Admin].Password);
        }

        public void Login(string userName, string password)
        {
            TestApiClient.Authenticator = new TestApiAuthenticator(TestConfiguration.Settings.BaseUri, userName, password);
        }
    }
}