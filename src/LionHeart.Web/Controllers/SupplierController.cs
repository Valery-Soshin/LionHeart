using LionHeart.Core.Dtos.Company;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Models.Supplier;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace LionHeart.Web.Controllers;

public class SupplierController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ICompanyService _companyService;
    private readonly ILogger<SupplierController> _logger;

    public SupplierController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              ICompanyService companyService,
                              ILogger<SupplierController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _companyService = companyService;
        _logger = logger;
    }

    public IActionResult ShowRegistrationInfo()
    {
        return View();
    }

    [HttpGet]
    public IActionResult RegisterSupplier()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> RegisterSupplier(RegisterSupplierViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        if (await _userManager.IsInRoleAsync(user, "Supplier"))
            return RedirectToAction("Register");

        var addCompanyDto = new AddCompanyDto()
        {
            Name = model.CompanyName,
            UserId = user.Id
        };
        var companyServiceResult = await _companyService.Add(addCompanyDto);
        if (companyServiceResult.IsFaulted) return BadRequest(companyServiceResult.ErrorMessage);
        
        var userManagerResult = await _userManager.AddToRoleAsync(user, "Supplier");
        if (!userManagerResult.Succeeded) return RedirectToAction("Register");
        await _signInManager.RefreshSignInAsync(user);

        _logger.LogInformation("User '{email}' has been assigned role 'Customer'", user.Email);
        return Redirect("/Products");
    }
}