using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IMenuItemRepository
    {
        Task CreateMenuItem(MenuItem menuItem);
        Task DeleteMenuItem(MenuItem menuItem);
        Task<List<MenuItem>> GetAllMenuItems();
        Task<MenuItem> GetMenuItemById(string id);
        Task UpdateMenu(MenuItem menuItem);
    }
}