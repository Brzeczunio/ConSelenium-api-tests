using Microsoft.Extensions.Configuration;

namespace ConSelenium.Settings
{
    public class TestConfiguration
    {
        public static TestApiSettings Settings => AppSettings.Value;

        private static Lazy<TestApiSettings> AppSettings => new Lazy<TestApiSettings>(() => GetSettings());

        private static TestApiSettings GetSettings()
        {
            var settings = new TestApiSettings();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            configuration.Bind(settings);

            return settings;
        }
    }
}
