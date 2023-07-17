using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineCoffeeShopDBContext _dbContext;

        public OrderRepository(OnlineCoffeeShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateOrder(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrder(Order order)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            var order = await _dbContext.Orders
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => id == x.Id);
            return order!;
        }

        public async Task<List<Order>> GetOrdersByDate(DateTime date)
        {
            var orders = await _dbContext.Orders.Where(x => date.Day == x.OrderDate.Day)
                .Include(x => x.Customer)
                .ToListAsync();
            return orders!;
        }
    }
}
