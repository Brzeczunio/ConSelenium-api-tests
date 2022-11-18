namespace ConSelenium.Api.Client.Models.Requests
{
    public class OrderRequest
    {
        public ICollection<OrderProductRequest> OrderProducts { get; set; }
    }
}
