using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlineCoffeeShop.Domain.Models;

namespace OnlineCoffeeShop.Infrastructure
{
    public class OnlineCoffeeShopDBInitializer
    {
        public readonly static string DATAPATH = @"OnlineCoffeeShop.Infrastructure\Data\";

        public static async Task Seed(IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<OnlineCoffeeShopDBContext>()!;
            if (context.Customers.Any())
                return;
            var currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
            if (currentDirectory == null)
                return;
            string filePath = Path.Combine(currentDirectory.FullName, DATAPATH);
            if (context == null || await context.Database.EnsureCreatedAsync())
                return;

            // JSON seeding of data

            if (!context.MenuItems.Any())
            {
                string fileName = Path.Combine(filePath, "menuItems.json");
                var read = File.ReadAllText(fileName);
                var data = JsonConvert.DeserializeObject<List<MenuItem>>(read);
                await context.MenuItems.AddRangeAsync(data!);
                await context.SaveChangesAsync();
            }
        }
    }
}
