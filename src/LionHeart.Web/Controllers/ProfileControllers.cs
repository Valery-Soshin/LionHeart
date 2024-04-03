using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IFavoriteProductService _favoriteProductService;
    private readonly UserManager<User> _userManager;

    public ProfileController(IFavoriteProductService favoriteProductService,
                             UserManager<User> userManager)
    {
        _favoriteProductService = favoriteProductService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> ShowNotifications()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ShowFavorites()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var result = await _favoriteProductService.GetAllByUserId(userId);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        var favoriteProducts = result.Data;
        if (favoriteProducts is null) return BadRequest();

        return View(favoriteProducts);
    }

    [HttpGet]
    public async Task<IActionResult> ShowProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var model = new ShowProfileViewModel()
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
    public async Task<IActionResult> EditProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var model = new EditProfileViewModel
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
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
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
                    return Redirect("/Profile/EditProfile");
                }
            }
            await _userManager.UpdateAsync(user);
            return Redirect("/Profile/ShowProfile");
        }
        return Redirect("/Profile/EditProfile");
    }
}