namespace OnlineCoffeeShop.Domain.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrderNumber { get; set; } = Guid.NewGuid().ToString();
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            Customer = new Customer();
            OrderItems = new List<OrderItem>();
        }
    }

}
