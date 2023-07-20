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

        public async Task<int> AddToCart(string shoppingCartId, string menuItemId)
        {
            try
            {
                //Check if menuItem is already in cart.
                var item = await _shoppingCartItemRepository.GetShoppingCartItemByMenuId(shoppingCartId, menuItemId);

                bool isInCart = item != null;

                if (!isInCart)
                {
                    var itemToAdd = await _menuItemRepository.GetMenuItemById(menuItemId);

                    var shoppingCartItem = new ShoppingCartItem
                    {
                        MenuItem = itemToAdd,
                        ShoppingCartId = shoppingCartId,
                        Quantity = 1
                    };

                    await _shoppingCartItemRepository.AddToCart(shoppingCartItem);
                    return shoppingCartItem.Quantity;
                }

                item!.Quantity++;
                await _shoppingCartItemRepository.UpdateShoppingCartItem(item);
                return item.Quantity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding to cart.", ex);
            }
        }

        public async Task<ResponseDTO<List<ShoppingCartItem>>> GetShoppingCartItems(string shoppingCartId)
        {
            try
            {
                var shoppingCartItems = await _shoppingCartItemRepository.GetShoppingCartItems(shoppingCartId);

                if (shoppingCartItems == null || shoppingCartItems.Count < 1)
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

        public async Task<int> RemoveFromCart(string shoppingCartId, string menuItemId)
        {
            try
            {
                //Check if menuItem is in cart.
                var item = await _shoppingCartItemRepository.GetShoppingCartItemByMenuId(shoppingCartId, menuItemId);

                bool isInCart = item != null;

                if (isInCart && item!.Quantity > 1)
                {
                    item!.Quantity--;
                    await _shoppingCartItemRepository.UpdateShoppingCartItem(item);
                    return item.Quantity;
                }

                await _shoppingCartItemRepository.DeleteShoppingCartItem(item!);
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding to cart.", ex);
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
