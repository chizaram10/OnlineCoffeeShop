using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Core.Interfaces;
using OnlineCoffeeShop.UI.Models;
using System.Diagnostics;

namespace OnlineCoffeeShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            var response = await _menuItemService.GetAllMenuItems();
            homeViewModel.MenuItems = response!;
            return View(homeViewModel);
        }
    }
}