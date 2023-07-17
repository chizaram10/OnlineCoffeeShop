using OnlineCoffeeShop.Core.Implementations;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Infrastructure.Repositories;

namespace OnlineCoffeeShop.UI.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IMenuItemService, MenuItemService>();

            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderItemService, OrderItemService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
            services.AddScoped<IShoppingCartItemService, ShoppingCartService>();
        }
    }
}
