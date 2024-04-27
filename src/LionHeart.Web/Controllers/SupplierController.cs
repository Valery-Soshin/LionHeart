using LionHeart.Core.Dtos.Company;
using LionHeart.Core.Helpers;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Helpers;
using LionHeart.Web.Models.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class SupplierController : MainController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ICompanyService _companyService;

    public SupplierController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              ICompanyService companyService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _companyService = companyService;
    }

    [HttpGet]
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
        if (!ModelState.IsValid) return View();

        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        if (await _userManager.IsInRoleAsync(user, RoleNameHelper.Supplier))
            return Redirect(RedirectHelper.SUPPLIER_REGISTER_SUPPLIER);

        var addCompanyDto = new AddCompanyDto()
        {
            Name = model.CompanyName,
            UserId = user.Id
        };
        var companyServiceResult = await _companyService.Add(addCompanyDto);
        if (companyServiceResult.IsFaulted) return Warning(companyServiceResult.ErrorMessages, true);
        
        var userManagerResult = await _userManager.AddToRoleAsync(user, RoleNameHelper.Supplier);
        var errorMessages = userManagerResult.Errors.Select(e => e.Description).ToList();
        if (!userManagerResult.Succeeded) return Warning(errorMessages, true);

        await _signInManager.RefreshSignInAsync(user);

        return Redirect(RedirectHelper.PRODUCTS_INDEX);
    }
}