using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItem>> GetOrderItemsByOrderId(string orderId);
    }
}