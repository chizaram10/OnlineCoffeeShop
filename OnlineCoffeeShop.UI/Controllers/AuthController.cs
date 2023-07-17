using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Core.DTOs;
using OnlineCoffeeShop.Core.Interfaces;

public class AuthController : Controller
{
    private readonly ICustomerService _customerService;

    public AuthController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = "")
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.LoginErrMsg = TempData["LoginErrMsg"];
        ViewBag.RegisterSuccessMsg = TempData["RegisterSuccessMsg"];

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO loginDTO, string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.LoginErrMsg = null;

        var loginResponse = await _customerService.Login(loginDTO);

        if (loginResponse == null)
        {
            ViewBag.LoginErrMsg = new string[] { "Invalid login credentials" };
            return View(loginDTO);
        }

        HttpContext.Session.SetString("IsAuthenticated", "true");
        HttpContext.Session.SetString("Username", loginDTO.Email);

        TempData["RegisterSuccessMsg"] = null;
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ViewResult Register()
    {
        ViewBag.RegisterErrMsg = TempData["RegisterErrMsg"];

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
        {
            TempData["RegisterErrMsg"] = true;
            return View(registerDTO);
        }

        var registerResponse = await _customerService.CreateCustomer(registerDTO);

        if (registerResponse == null)
        {
            TempData["RegisterErrMsg"] = true;
            return View(registerDTO);
        }

        TempData["RegisterErrMsg"] = null;
        TempData["RegisterSuccessMsg"] = "Registration successful. Please log in to continue.";
        return RedirectToAction("Login", "Auth");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.SetString("IsAuthenticated", "false");
        HttpContext.Session.SetString("Username", "");
        return RedirectToAction("Index", "Home");
    }
}
