namespace Ecommerce.Api.Search.Model
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }

    }
}
