using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task AddToCart(ShoppingCartItem item);
        Task ClearCart(string shoppingCartId);
        Task<List<ShoppingCartItem>> GetShoppingCartItems(string shoppingCartId);
        Task<ShoppingCartItem> GetShoppingCartItemById(string id);
        decimal GetShoppingCartTotal(string shoppingCartId);
    }
}