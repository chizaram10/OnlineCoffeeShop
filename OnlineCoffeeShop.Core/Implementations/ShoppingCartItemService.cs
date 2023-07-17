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

        public async Task AddToCart(string menuItemId, string shoppingCartId)
        {
            try
            {
                var itemToAdd = await _menuItemRepository.GetMenuItemById(menuItemId);

                var shoppingCartItem = new ShoppingCartItem
                {
                    MenuItem = itemToAdd,
                    ShoppingCartId = shoppingCartId,
                    Quantity = 1
                };

                await _shoppingCartItemRepository.AddToCart(shoppingCartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting adding to cart.", ex);
            }
        }

        public async Task<int> RemoveFromCart(string menuItemId, string shoppingCartId)
        {
            try
            {
                var itemToAdd = await _menuItemRepository.GetMenuItemById(menuItemId);

                var shoppingCartItem = new ShoppingCartItem
                {
                    MenuItem = itemToAdd,
                    ShoppingCartId = shoppingCartId,
                    Quantity = 1
                };

                return await _shoppingCartItemRepository.RemoveFromCart(shoppingCartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting adding to cart.", ex);
            }
        }

        public Task<List<ShoppingCartItem>> GetShoppingCartItems(string shoppingCartId)
        {
            return _shoppingCartItemRepository.GetShoppingCartItems(shoppingCartId);
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
