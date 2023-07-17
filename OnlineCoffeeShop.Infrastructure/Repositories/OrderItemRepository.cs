using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OnlineCoffeeShopDBContext _dbContext;

        public OrderItemRepository(OnlineCoffeeShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderId(string id)
        {
            var orderItem = await _dbContext.OrderItems
                .Where(oi => id == oi.OrderId)
                .Include(oi => oi.MenuItem)
                .ToListAsync();

            return orderItem!;
        }

    }
}
