namespace OnlineCoffeeShop.Domain.Models
{
    public class ShoppingCartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string MenuItemId { get; set; } = string.Empty;
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; } = string.Empty;

        public ShoppingCartItem()
        {
            MenuItem = new MenuItem();
        }
    }
}
