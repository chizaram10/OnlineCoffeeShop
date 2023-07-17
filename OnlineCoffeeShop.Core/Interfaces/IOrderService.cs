using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrder(ShoppingCart shoppingCart, decimal amount, 
            string email);
        Task<List<OrderDTO>> GetOrdersByDate(DateTime date);
        Task CancelOrder(string orderId);
    }
}