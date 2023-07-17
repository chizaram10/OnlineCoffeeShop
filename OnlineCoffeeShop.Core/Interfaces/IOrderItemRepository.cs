using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Core.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetOrderItemsByOrderId(string id);
    }
}