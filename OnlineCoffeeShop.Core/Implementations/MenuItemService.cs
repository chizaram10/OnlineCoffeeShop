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

        public async Task<MenuItem> GetMenuItemById(string id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetMenuItemById(id);

                return menuItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to find menu item", ex);
            }
        }

        public async Task<List<MenuItem>> GetAllMenuItems()
        {
            try
            {
                var allMenuItems = await _menuItemRepository.GetAllMenuItems();
                return allMenuItems;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
