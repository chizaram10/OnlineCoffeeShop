using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Implementations
{
    public class ShoppingCartService : IShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public ShoppingCartService(IShoppingCartItemRepository shoppingCartItemRepository,
            IMenuItemRepository menuItemRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task AddToCart(string menuItemId, string shoppingCartId, int quantity)
        {
            try
            {
                var itemToAdd = await _menuItemRepository.GetMenuItemById(menuItemId);

                var shoppingCartItem = new ShoppingCartItem
                {
                    MenuItem = itemToAdd,
                    ShoppingCartId = shoppingCartId,
                    Quantity = quantity
                };

                await _shoppingCartItemRepository.AddToCart(shoppingCartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting adding to cart.", ex);
            }
        }

        public async Task<ResponseDTO<List<ShoppingCartItem>>> GetShoppingCartItems(string shoppingCartId)
        {
            try
            {
                var shoppingCartItems = await _shoppingCartItemRepository.GetShoppingCartItems(shoppingCartId);
                
                if(shoppingCartItems == null || shoppingCartItems.Count < 1)
                {
                    return ResponseDTO<List<ShoppingCartItem>>.Fail(new string[] { "No items in your shopping cart" });
                }

                return ResponseDTO<List<ShoppingCartItem>>.Success(shoppingCartItems);
            }
            catch
            {
                return ResponseDTO<List<ShoppingCartItem>>.Fail(new string[] { "No items in your shopping cart" });
            }
        }

        public Task ClearCart(string shoppingCartId)
        {
            return _shoppingCartItemRepository.ClearCart(shoppingCartId);
        }

        public decimal GetShoppingCartTotal(string shoppingCartId)
        {
            return _shoppingCartItemRepository.GetShoppingCartTotal(shoppingCartId);
        }
    }

}
