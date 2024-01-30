namespace Ecommerce.Api.Search.Model
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
