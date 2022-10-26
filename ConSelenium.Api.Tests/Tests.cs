using ConSelenium.Api.Client;
using ConSelenium.Settings;

namespace ConSelenium.Api.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var abc = new TestApiContext();
            var ASD = new TestApiClient(abc.TestApiClient);
            Assert.Pass();
        }
    }
}