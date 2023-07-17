using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;
using OnlineCoffeeShop.UI.Extensions;
using OnlineCoffeeShop.UI.Models;

namespace OnlineCoffeeShop.UI.Controllers
{
	public class OrderController : Controller
	{
        private readonly IOrderService _orderService;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly UserManager<Customer> _userManager;

        public OrderController(IOrderService orderService,
            IShoppingCartItemService shoppingCartItemService, UserManager<Customer> userManager)
        {
            _orderService = orderService;
            _shoppingCartItemService = shoppingCartItemService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Checkout()
        {
            bool isAuthenticated = (HttpContext.Session.GetString("IsAuthenticated") == "true");

            if (!isAuthenticated)
            {
                return RedirectToAction("Login", "Auth");
            }

            var shoppingCart = ShoppingCartExtension.GetCart(HttpContext.RequestServices);
            var items = await _shoppingCartItemService.GetShoppingCartItems(shoppingCart.Id);
            shoppingCart.ShoppingCartItems = items;
            var email = HttpContext.Session.GetString("Username")!;


            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some items first");
            }

            var shoppingcartTotal = _shoppingCartItemService.GetShoppingCartTotal(shoppingCart.Id);

            var response = await _orderService.CreateOrder(shoppingCart, shoppingcartTotal, email);
            await _shoppingCartItemService.ClearCart(shoppingCart.Id);

            return View(response);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your patronage. Your order will arrive soon!";
            return View();
        }

        public async Task<IActionResult> CancelOrder(string id)
        {
            ViewBag.CancelCompleteMessage = "Your order has been cancelled!";
            await _orderService.CancelOrder(id);
            return View();
        }

        public async Task<IActionResult> TodaysOrders()
        {
            var todaysOrdersVM = new TodaysOrdersViewModel();
            var response = await _orderService.GetOrdersByDate(DateTime.UtcNow);
            todaysOrdersVM.Orders = response!;
            return View(todaysOrdersVM);
        }
    }
}
