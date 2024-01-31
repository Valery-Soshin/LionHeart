using LionHeart.Core.Models;
using LionHeart.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LionHeart.Web.Controllers;

[Authorize(Roles = "Customer")]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;

    public ProfileController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        ViewBag.User = user;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);

        if (ModelState.IsValid)
        {
            if (user is not null)
            {
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;

                if (model.Password is not null && model.CurrentPassword is not null)
                {
                    var result = await _userManager
                        .ChangePasswordAsync(user, model.CurrentPassword, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                    ViewBag.User = user;
                    ViewBag.ShowModal = "true";
                    return View("Edit");
                }

                await _userManager.UpdateAsync(user);

                return RedirectToAction("Index", new { UserId = model.Id});
            }
        }

        ViewBag.User = user;
        ViewBag.ShowModal = "true";
        return View("Edit");
    }

    [HttpGet]
    public async Task<IActionResult> Remove(string userId)
    {
        return null; 
    }

    [HttpGet]
    public IActionResult EditError()
    {
        return View();
    }
}