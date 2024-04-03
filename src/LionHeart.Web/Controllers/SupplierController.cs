using LionHeart.Core.Models;
using LionHeart.Web.Models.Supplier;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class SupplierController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              ILogger<SupplierController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        if (await _userManager.IsInRoleAsync(user, "Supplier"))
            return RedirectToAction("Register");

        var result = await _userManager.AddToRoleAsync(user, "Supplier");
        if (!result.Succeeded) return RedirectToAction("Register");

        await _signInManager.RefreshSignInAsync(user);

        _logger.LogInformation("User '{email}' has been assigned role 'Customer'", user.Email);
        return Redirect("/Products");
    }
}