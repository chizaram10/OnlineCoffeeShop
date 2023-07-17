namespace OnlineCoffeeShop.Domain.Models
{
    public class ShoppingCart
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }
    }
}
