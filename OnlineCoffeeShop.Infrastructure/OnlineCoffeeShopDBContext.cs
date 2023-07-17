using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Infrastructure
{
    public class OnlineCoffeeShopDBContext : IdentityDbContext
    {
        public OnlineCoffeeShopDBContext(DbContextOptions<OnlineCoffeeShopDBContext> options) : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}