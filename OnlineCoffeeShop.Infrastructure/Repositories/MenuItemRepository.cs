using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly OnlineCoffeeShopDBContext _dbContext;

        public MenuItemRepository(OnlineCoffeeShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateMenuItem(MenuItem menuItem)
        {
            await _dbContext.MenuItems.AddAsync(menuItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MenuItem> GetMenuItemById(string id)
        {
            var menuItem = await _dbContext.MenuItems.FirstOrDefaultAsync(m => id == m.Id);
            return menuItem!;
        }

        public async Task<List<MenuItem>> GetAllMenuItems()
        {
            var menuItems = await _dbContext.MenuItems.ToListAsync();
            return menuItems;
        }

        public async Task UpdateMenu(MenuItem menuItem)
        {
            _dbContext.Entry(menuItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMenuItem(MenuItem menuItem)
        {
            _dbContext.MenuItems.Remove(menuItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
