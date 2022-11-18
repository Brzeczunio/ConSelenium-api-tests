using ConSelenium.Api.Client.Models.Requests;

namespace ConSelenium.Api.Client.Builders
{
    public class OrderProductBuilder
    {
        private OrderProductRequest _orderProduct { get; set; }

        public OrderProductBuilder()
        {
            _orderProduct = new OrderProductRequest();
        }

        public OrderProductBuilder AddProductId(int productId)
        {
            _orderProduct.ProductId = productId;

            return this;
        }

        public OrderProductBuilder AddQuantity(int quantity)
        {
            _orderProduct.Quantity = quantity;

            return this;
        }

        public OrderProductRequest Build() => _orderProduct;
    }
}
