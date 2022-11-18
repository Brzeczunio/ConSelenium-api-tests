namespace ConSelenium.Api.Client.Models.Responses
{
    public class Order
    {
        public DateTime CreationDate { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
