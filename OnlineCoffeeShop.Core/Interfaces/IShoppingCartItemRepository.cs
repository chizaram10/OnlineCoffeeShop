using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task AddToCart(ShoppingCartItem item);
        Task ClearCart(string shoppingCartId);
        Task<List<ShoppingCartItem>> GetShoppingCartItems(string shoppingCartId);
        decimal GetShoppingCartTotal(string shoppingCartId);
        Task<int> RemoveFromCart(ShoppingCartItem item);
    }
}