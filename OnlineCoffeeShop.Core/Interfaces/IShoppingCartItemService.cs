using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IShoppingCartItemService
    {
        Task<int> AddToCart(string shoppingCartId, string menuItemId);
        Task<int> RemoveFromCart(string shoppingCartId, string menuItemId);
        Task ClearCart(string shoppingCartId);
        Task<ResponseDTO<List<ShoppingCartItem>>> GetShoppingCartItems(string shoppingCartId);
        decimal GetShoppingCartTotal(string shoppingCartId);
    }
}