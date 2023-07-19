using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IMenuItemService
    {
        Task<ResponseDTO<List<MenuItem>>> GetAllMenuItems();
        Task<ResponseDTO<MenuItem>> GetMenuItemById(string id);
    }
}