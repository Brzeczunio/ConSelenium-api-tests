using ConSelenium.Common;
using ConSelenium.Settings.Base;

namespace ConSelenium.Settings
{
    public class TestApiContext : BaseContext
    {
        public LoggerDecoratedRestClient TestApiClient { get; set; }

        public TestApiContext() : base()
        {
            TestApiClient = new LoggerDecoratedRestClient(TestConfiguration.Settings.BaseUri);
        }
    }
}