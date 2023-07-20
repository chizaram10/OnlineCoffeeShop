namespace OnlineCoffeeShop.UI.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthenticationConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "DefaultAuthenticationScheme";
                options.DefaultChallengeScheme = "DefaultAuthenticationScheme";
            }).AddCookie("DefaultAuthenticationScheme", options =>
            {
                options.Cookie.Name = "AuthCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.LoginPath = "/Auth/Login";
            });
        }
    }
}
