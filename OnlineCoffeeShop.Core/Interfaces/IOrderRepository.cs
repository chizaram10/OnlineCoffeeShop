using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrder(Order order);
        Task<Order> GetOrderById(string id);
        Task DeleteOrder(Order order);
        Task<List<Order>> GetOrdersByDate(DateTime date);
    }
}