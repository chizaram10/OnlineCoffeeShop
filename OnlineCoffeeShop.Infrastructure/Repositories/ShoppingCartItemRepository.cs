using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Infrastructure.Repositories
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly OnlineCoffeeShopDBContext _dbContext;

        public ShoppingCartItemRepository(OnlineCoffeeShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddToCart(ShoppingCartItem item)
        {
            await _dbContext.ShoppingCartItems.AddAsync(item!);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems(string shoppingCartId)
        {
            var shoppingCartItems = await _dbContext.ShoppingCartItems
                .Where(si => shoppingCartId == si.ShoppingCartId)
                .Include(si => si.MenuItem)
                .ToListAsync();

            return shoppingCartItems!;
        }

        public async Task<ShoppingCartItem> GetShoppingCartItemByMenuId(string shoppingCartId, string menuItemId)
        {
            var shoppingCartItem = await _dbContext.ShoppingCartItems
                .Include(si => si.MenuItem)
                .FirstOrDefaultAsync(si => shoppingCartId == si.ShoppingCartId && menuItemId == si.MenuItemId);

            return shoppingCartItem!;
        }

        public async Task UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _dbContext.Entry(shoppingCartItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }



        public async Task ClearCart(string shoppingCartId)
        {
            var shoppingCartItems = await _dbContext.ShoppingCartItems
                .Where(si => shoppingCartId == si.ShoppingCartId)
                .ToListAsync();

            _dbContext.ShoppingCartItems.RemoveRange(shoppingCartItems);

            await _dbContext.SaveChangesAsync();
        }

        public decimal GetShoppingCartTotal(string shoppingCartId)
        {
            var total = _dbContext.ShoppingCartItems.Where(c => shoppingCartId == c.ShoppingCartId)
                .Select(c => c.MenuItem.Price * c.Quantity).Sum();
            return total;
        }

        public async Task DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
