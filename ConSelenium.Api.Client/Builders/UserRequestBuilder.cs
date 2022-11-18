using ConSelenium.Api.Client.Models;
using ConSelenium.Api.Client.Models.Requests;

namespace ConSelenium.Api.Client.Builders
{
    public class UserRequestBuilder
    {
        private UserRequest _user { get; set; }

        public UserRequestBuilder()
        {
            _user = new UserRequest();
            _user.Address = new Address();
        }

        public UserRequestBuilder AddName(string name)
        {
            _user.Name = name;

            return this;
        }

        public UserRequestBuilder AddSurname(string surname)
        {
            _user.Surname = surname;

            return this;
        }

        public UserRequestBuilder AddUserName(string userName)
        {
            _user.UserName = userName;

            return this;
        }

        public UserRequestBuilder AddPassword(string Password)
        {
            _user.Password = Password;

            return this;
        }        
        
        public UserRequestBuilder AddEmail(string email)
        {
            _user.Email = email;

            return this;
        }

        public UserRequestBuilder AddAddress(Address address)
        {
            _user.Address = address;

            return this;
        }

        public UserRequest Build() => _user;
    }
}
