using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IShoppingCartItemService
    {
        Task AddToCart(string menuItemId, string shoppingCartId);
        Task ClearCart(string shoppingCartId);
        Task<List<ShoppingCartItem>> GetShoppingCartItems(string shoppingCartId);
        decimal GetShoppingCartTotal(string shoppingCartId);
        Task<int> RemoveFromCart(string menuItemId, string shoppingCartId);
    }
}