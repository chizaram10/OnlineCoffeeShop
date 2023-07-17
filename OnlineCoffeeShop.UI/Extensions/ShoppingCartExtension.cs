using OnlineCoffeeShop.Domain.Models;
using OnlineCoffeeShop.Infrastructure;

namespace OnlineCoffeeShop.UI.Extensions
{
    public class ShoppingCartExtension
    {
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext!.Session!;
            var context = services.GetService<OnlineCoffeeShopDBContext>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();
            session.SetString("Id", cartId);
            return new ShoppingCart { Id = cartId };
        }
    }
}
