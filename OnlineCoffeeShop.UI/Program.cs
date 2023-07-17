using Microsoft.AspNetCore.Identity;
using OnlineCoffeeShop.Domain.Models;
using OnlineCoffeeShop.Infrastructure;
using OnlineCoffeeShop.UI.Extensions;

namespace OnlineCoffeeShop.UI
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContextExtension(builder.Environment, builder.Configuration);
            builder.Services.AddIdentityConfiguration();
            builder.Services.AddServicesExtension(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();
            builder.Services.AddAuthenticationConfiguration();
            builder.Services.AddAuthorization();
			builder.Services.AddScoped<ShoppingCart>(sp => ShoppingCartExtension.GetCart(sp));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await OnlineCoffeeShopDBInitializer.Seed(app);

            app.Run();
        }
    }
}