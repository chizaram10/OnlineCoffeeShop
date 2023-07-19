using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IShoppingCartItemService
    {
        Task AddToCart(string menuItemId, string shoppingCartId, int quantity);
        Task ClearCart(string shoppingCartId);
        Task<ResponseDTO<List<ShoppingCartItem>>> GetShoppingCartItems(string shoppingCartId);
        decimal GetShoppingCartTotal(string shoppingCartId);
    }
}