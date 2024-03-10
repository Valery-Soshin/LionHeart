using LionHeart.Core.Models;
using LionHeart.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;

    public ProfileController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var model = new IndexViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            PersonalDiscount = user.PersonalDiscount
        };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var model = new EditUserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            PersonalDiscount = user.PersonalDiscount
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> EditUser(EditUserViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        if (ModelState.IsValid)
        {
            user.UserName = model.Email;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(model.Password) && !string.IsNullOrWhiteSpace(model.CurrentPassword))
            {
                var result = await _userManager
                    .ChangePasswordAsync(user, model.CurrentPassword, model.Password);

                if (!result.Succeeded)
                {
                    return Redirect("/Profile/EditUser");
                }
            }
            await _userManager.UpdateAsync(user);
            return Redirect("/Profile");
        }
        return Redirect("/Profile/EditUser");
    }
}