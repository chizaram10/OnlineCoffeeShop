using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.UI.Models
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; } = new ShoppingCart();
        public decimal ShoppingCartTotal { get; set; }
    }
}
