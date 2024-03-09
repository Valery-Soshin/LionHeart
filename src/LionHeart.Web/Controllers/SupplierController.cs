using LionHeart.Core.Models;
using LionHeart.Web.Models.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class SupplierController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(UserManager<User> userManager,
                                          ILogger<SupplierController> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        if (await _userManager.IsInRoleAsync(user, "Supplier"))
        {
            return RedirectToAction("ShowRegistrationInfo");
        }

        var result = await _userManager.AddToRoleAsync(user, "Supplier");
        if (!result.Succeeded) return RedirectToAction("ShowRegistrationInfo");
        
        _logger.LogInformation("User '{email}' has been assigned role 'Customer'", user.Email);
        return Ok("Good");
    }
}