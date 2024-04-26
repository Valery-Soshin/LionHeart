using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class NotificationsController : MainController
{
    private readonly INotificationService _notificationService;
    private readonly UserManager<User> _userManager;

    public NotificationsController(INotificationService notificationService,
                                   UserManager<User> userManager)
    {
        _notificationService = notificationService;
        _userManager = userManager;
    }

    public async Task<IActionResult> DeleteNotification([FromBody]string id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var notificationServiceResult = await _notificationService.Remove(id);
        if (notificationServiceResult.IsFaulted) return BadRequest(notificationServiceResult.ErrorMessages);

        return Ok();
    }
    public async Task<IActionResult> DeleteAllNotifications()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();
        
        var notificationServiceResult = await _notificationService.RemoveAll(userId);
        if (notificationServiceResult.IsFaulted) return Warning(notificationServiceResult.ErrorMessages);

        return Redirect(RedirectHelper.PROFILE_SHOW_NOTIFICATIONS);
    }
}