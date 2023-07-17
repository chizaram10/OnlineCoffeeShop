using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IMenuItemService
    {
        Task<List<MenuItem>> GetAllMenuItems();
        Task<MenuItem> GetMenuItemById(string id);
    }
}