using ConSelenium.Api.Client.Models.Requests;

namespace ConSelenium.Api.Client.Builders
{
    public class ProductRequestBuilder
    {
        private ProductRequest _product { get; set; }

        public ProductRequestBuilder()
        {
            _product = new ProductRequest();
        }

        public ProductRequestBuilder AddName(string name)
        {
            _product.Name = name;

            return this;
        }

        public ProductRequestBuilder AddDescription(string description)
        {
            _product.Description = description;

            return this;
        }

        public ProductRequestBuilder AddStock(int stock)
        {
            _product.Stock = stock;

            return this;
        }

        public ProductRequestBuilder AddPrice(double price)
        {
            _product.Price = price;

            return this;
        }

        public ProductRequest Build() => _product;
    }
}
