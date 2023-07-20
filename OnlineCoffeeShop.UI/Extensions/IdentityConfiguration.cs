using Microsoft.AspNetCore.Identity;
using OnlineCoffeeShop.Domain.Models;
using OnlineCoffeeShop.Infrastructure;

namespace OnlineCoffeeShop.UI.Extensions
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentityCore<Customer>()
                .AddEntityFrameworkStores<OnlineCoffeeShopDBContext>()
                .AddSignInManager<SignInManager<Customer>>()
                .AddUserManager<UserManager<Customer>>(); ;
        }
    }
}
