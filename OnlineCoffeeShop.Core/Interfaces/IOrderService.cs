using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseDTO<OrderDTO>> CreateOrder(ShoppingCart shoppingCart, decimal amount, 
            string email);
        Task<ResponseDTO<List<OrderDTO>>> GetOrdersByDate(DateTime date);
        Task<ResponseDTO<OrderDTO>> CancelOrder(string orderId);
    }
}