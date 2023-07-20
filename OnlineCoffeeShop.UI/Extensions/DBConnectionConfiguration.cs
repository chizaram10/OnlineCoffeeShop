using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.ConfigOptions;
using OnlineCoffeeShop.Infrastructure;

namespace OnlineCoffeeShop.UI.Extensions
{
    public static class DBConnectionConfiguration
    {
        public static void AddDbContextExtension(this IServiceCollection services, IWebHostEnvironment env,
            IConfiguration config)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);


            var connectionString = new ConnectionString();

            services.AddDbContextPool<OnlineCoffeeShopDBContext>(options =>
            {
                if (env.IsProduction())
                {
                    connectionString.DefaultConnection = Environment.GetEnvironmentVariable("ConnectionString")!;
                }
                else
                {
                    config.Bind(nameof(connectionString), connectionString);
                }

                options.UseNpgsql(connectionString.DefaultConnection);
            });
        }
    }
}
