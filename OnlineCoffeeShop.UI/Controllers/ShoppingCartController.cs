using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;
using OnlineCoffeeShop.UI.Extensions;
using OnlineCoffeeShop.UI.Models;

namespace OnlineCoffeeShop.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IShoppingCartItemService _shoppingCartItemService;

        public ShoppingCartController(IShoppingCartItemService shoppingCartItemService,
            IMenuItemService menuItemService)
        {
            _shoppingCartItemService = shoppingCartItemService;
            _menuItemService = menuItemService;
        }

        public async Task<ViewResult> Index()
        {
            var shoppingCart = ShoppingCartExtension.GetCart(HttpContext.RequestServices);
            var response = await _shoppingCartItemService.GetShoppingCartItems(shoppingCart.Id);
            shoppingCart.ShoppingCartItems = response.Data!;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = _shoppingCartItemService.GetShoppingCartTotal(shoppingCart.Id)
            };

            return View(shoppingCartViewModel);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(string id, int quantity)
        {
            var shoppingCart = ShoppingCartExtension.GetCart(HttpContext.RequestServices);
            await _shoppingCartItemService.AddToCart(id, shoppingCart.Id, quantity);
            
            return RedirectToAction("Index", "Home");
        }
    }
}
