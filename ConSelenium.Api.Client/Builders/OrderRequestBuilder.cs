using ConSelenium.Api.Client.Models.Requests;

namespace ConSelenium.Api.Client.Builders
{
    public class OrderRequestBuilder
    {
        private OrderRequest _order { get; set; }

        public OrderRequestBuilder()
        {
            _order = new OrderRequest();
            _order.OrderProducts = new List<OrderProductRequest>();
        }

        public OrderRequestBuilder AddOrderProduct(OrderProductRequest orderProduct)
        {
            _order.OrderProducts.Add(orderProduct);

            return this;
        }

        public OrderRequest Build() => _order;
    }
}
