using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Components;

public class NotificationCounterViewComponent : ViewComponent
{
    private readonly INotificationService _notificationService;
    private readonly UserManager<User> _userManager;

    public NotificationCounterViewComponent(INotificationService notificationService,
                                            UserManager<User> userManager)
    {
        _notificationService = notificationService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = _userManager.GetUserId(UserClaimsPrincipal);
        if (userId is null) return View(-1);

        var notificationServiceResult = await _notificationService.Count(userId);
        if (notificationServiceResult.IsFaulted) return View(-1);
        var count = notificationServiceResult.Data;
        return View(count);
    }
}