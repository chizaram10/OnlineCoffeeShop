using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task AddToCart(ShoppingCartItem item);
        Task ClearCart(string shoppingCartId);
        Task<List<ShoppingCartItem>> GetShoppingCartItems(string shoppingCartId);
        Task<ShoppingCartItem> GetShoppingCartItemByMenuId(string shoppingCartId, string menuItemId);
        decimal GetShoppingCartTotal(string shoppingCartId);
        Task UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        Task DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem);
    }
}