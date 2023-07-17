namespace OnlineCoffeeShop.Domain.Models
{
    public class OrderItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public Order Order { get; set; }
        public string MenuItemId { get; set; } = string.Empty;
        public MenuItem MenuItem { get; set; }

        public OrderItem()
        {
            Order = new Order();
            MenuItem = new MenuItem();
        }
    }
}
