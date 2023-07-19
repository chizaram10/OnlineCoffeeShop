using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Implementations
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<ResponseDTO<MenuItem>> GetMenuItemById(string id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetMenuItemById(id);

                if (menuItem == null)
                {
                    return ResponseDTO<MenuItem>.Fail(new string[] { "Menu item not found." });
                }

                return ResponseDTO<MenuItem>.Success(menuItem);
            }
            catch
            {
                return ResponseDTO<MenuItem>.Fail(new string[] { "Menu item not found." });
            }
        }

        public async Task<ResponseDTO<List<MenuItem>>> GetAllMenuItems()
        {
            try
            {
                var allMenuItems = await _menuItemRepository.GetAllMenuItems();

                if (allMenuItems == null || allMenuItems.Count < 1)
                {
                    return ResponseDTO<List<MenuItem>>.Fail(new string[] { "No menu items found." });
                }

                return ResponseDTO<List<MenuItem>>.Success(allMenuItems);
            }
            catch
            {
                return ResponseDTO<List<MenuItem>>.Fail(new string[] { "No menu items found." });
            }
        }
    }
}
