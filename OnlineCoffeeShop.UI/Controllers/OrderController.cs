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
            var response = await _shoppingCartItemService.GetShoppingCartItems(shoppingCart.Id);
            shoppingCart.ShoppingCartItems = response.Data!;
            var email = HttpContext.Session.GetString("Username")!;


            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some response first");
            }

            var shoppingcartTotal = _shoppingCartItemService.GetShoppingCartTotal(shoppingCart.Id);

            var response2 = await _orderService.CreateOrder(shoppingCart, shoppingcartTotal, email);
            await _shoppingCartItemService.ClearCart(shoppingCart.Id);

            return View(response2.Data!);
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
            todaysOrdersVM.Orders = response.Data!;
            return View(todaysOrdersVM);
        }
    }
}
