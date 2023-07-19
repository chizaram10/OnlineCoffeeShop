using OnlineCoffeeShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Core.Interfaces;

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
            var shoppingCartItem = await _dbContext.ShoppingCartItems
                .FirstOrDefaultAsync(s => item.Id == s.Id);

            if (shoppingCartItem == null)
            {
                await _dbContext.ShoppingCartItems.AddAsync(item!);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }

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

        public async Task<ShoppingCartItem> GetShoppingCartItemById(string id)
        {
            var shoppingCartItem = await _dbContext.ShoppingCartItems
                .Include(si => si.MenuItem)
                .FirstOrDefaultAsync(si => id == si.Id);

            return shoppingCartItem!;
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
    }
}
