using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.Domain.Models;
using OnlineCoffeeShop.UI.Extensions;
using OnlineCoffeeShop.UI.Models;

namespace OnlineCoffeeShop.UI.Components
{
	public class ShoppingCartSummary : ViewComponent
	{
		private readonly IShoppingCartItemService _shoppingCartItemService;

		public ShoppingCartSummary(IShoppingCartItemService shoppingCartItemService)
		{
			_shoppingCartItemService = shoppingCartItemService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var shoppingCart = ShoppingCartExtension.GetCart(HttpContext.RequestServices);
			var items = await _shoppingCartItemService.GetShoppingCartItems(shoppingCart.Id);
			shoppingCart.ShoppingCartItems = items;

			var shoppingCartViewModel = new ShoppingCartViewModel
			{
				ShoppingCart = shoppingCart,
				ShoppingCartTotal = _shoppingCartItemService.GetShoppingCartTotal(shoppingCart.Id)
			};

			return View("", shoppingCartViewModel);
		}
	}
}
