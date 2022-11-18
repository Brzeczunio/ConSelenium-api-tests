using ConSelenium.Common;
using ConSelenium.Common.Auth;
using ConSelenium.Settings.Enums;

namespace ConSelenium.Settings
{
    public class TestApiContext
    {
        public LoggerDecoratedRestClient TestApiClientV1 { get; set; }

        public TestApiContext() : base()
        {
            TestApiClientV1 = new LoggerDecoratedRestClient(TestConfiguration.Settings.BaseUri);
            TestApiClientV1.Authenticator = new TestApiAuthenticator(
                TestConfiguration.Settings.BaseUri, TestConfiguration.Settings.Users[User.Admin].Login, TestConfiguration.Settings.Users[User.Admin].Password);
        }

        public void Login(string userName, string password)
        {
            TestApiClientV1.Authenticator = new TestApiAuthenticator(TestConfiguration.Settings.BaseUri, userName, password);
        }
    }
}