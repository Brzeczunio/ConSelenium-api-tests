using ConSelenium.Api.Client.Models;

namespace ConSelenium.Api.Client.Builders
{
    public class AddressBuilder
    {
        private Address _address { get; set; }

        public AddressBuilder()
        {
            _address = new Address();
        }

        public AddressBuilder AddCity(string city)
        {
            _address.City = city;

            return this;
        }

        public AddressBuilder AddStreet(string street)
        {
            _address.Street = street;

            return this;
        }

        public AddressBuilder AddhouseNumber(string houseNumber)
        {
            _address.HouseNumber = houseNumber;

            return this;
        }

        public AddressBuilder AddPostalcode(string postalcode)
        {
            _address.Postalcode = postalcode;

            return this;
        }

        public Address Build() => _address;
    }
}
