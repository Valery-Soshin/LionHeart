using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Helpers;
using LionHeart.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class ProfileController : MainController
{
    private readonly IFavoriteProductService _favoriteProductService;
    private readonly IFeedbackService _feedbackService;
    private readonly INotificationService _notificationService;
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;

    public ProfileController(IFavoriteProductService favoriteProductService,
                             IFeedbackService feedbackService,
                             INotificationService notificationService,
                             IProductService productService,
                             UserManager<User> userManager)
    {
        _favoriteProductService = favoriteProductService;
        _feedbackService = feedbackService;
        _notificationService = notificationService;
        _productService = productService;
        _userManager = userManager;
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
    public async Task<IActionResult> ShowFavorites(int pageNumber = 1)
    {
        if (!ModelState.IsValid) return Warning(ModelState);

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var favoriteProductServiceResult = await _favoriteProductService.GetFavoritesByUserId(userId, pageNumber);
        if (favoriteProductServiceResult.IsFaulted) return Warning(favoriteProductServiceResult.ErrorMessages);
        var page = favoriteProductServiceResult.Value;

        return View(page);
    }

    [HttpGet]
    public async Task<IActionResult> ShowMyFeedbacks(int pageNumber = 1)
    {
        if (!ModelState.IsValid) return Warning(ModelState);

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var feedbackServiceResult = await _feedbackService.GetFeedbacksByUserId(userId, pageNumber);
        if (feedbackServiceResult.IsFaulted) return Warning(feedbackServiceResult.ErrorMessages);
        var page = feedbackServiceResult.Value;
     
        var model = new ShowMyFeedbacksViewModel()
        {
            PageNumber = page.PageNumber,
            HasPreviousPage = page.HasPreviousPage,
            HasNextPage = page.HasNextPage,
            Feedbacks = page.Entities.Select(e => new ShowMyFeedbacksItemViewModel
            {
                Id = e.Id,
                ProductName = e.Product!.Name,
                ImageName = e.Product.Image.FileName,
                Content = e.Content,
                Rating = (int)e.Rating,
                CreatedAt = e.CreatedAt
            }).ToList()
        };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ShowNotifications()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var notificationServiceResult = await _notificationService.GetNotificationsByUserId(userId);
        if (notificationServiceResult.IsFaulted) return Warning(notificationServiceResult.ErrorMessages);
        var notifications = notificationServiceResult.Value;

        var model = new ShowNotificationsViewModel
        {
            Notifications = notifications.Select(n => new ShowNotificationItemViewModel
            {
                Id = n.Id,
                UserId = n.UserId,
                Content = n.Content,
                LinkToAction = n.LinkToAction,
                CreatedAt = n.CreatedAt
            }).ToList()
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
        if (!ModelState.IsValid) return View();

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
                    return View();
                }
            }
            await _userManager.UpdateAsync(user);
            return Redirect(RedirectHelper.PROFILE_SHOW_PROFILE);
        }
        return View();
    }
}