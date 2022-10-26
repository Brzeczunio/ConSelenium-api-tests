using ConSelenium.Settings.Enums;
using ConSelenium.Settings.Models;

namespace ConSelenium.Settings
{
    public class TestApiSettings
    {
        public Uri BaseUri { get; set; }
        public Dictionary<UserCredentials, User> Users { get; set; }
    }
}
