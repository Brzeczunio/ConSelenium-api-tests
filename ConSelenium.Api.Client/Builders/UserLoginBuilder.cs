using ConSelenium.Api.Client.Models;

namespace ConSelenium.Api.Client.Builders
{
    public class UserLoginBuilder
    {
        private UserLogin _userLogin { get; set; }

        public UserLoginBuilder AddUserName(string userName)
        {
            _userLogin.UserName = userName;

            return this;
        }

        public UserLoginBuilder AddPassword(string Password)
        {
            _userLogin.Password = Password;

            return this;
        }

        public UserLogin Build() => _userLogin;
    }
}
