using LionHeart.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class NotificationsController : Controller
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<IActionResult> DeleteNotification([FromBody]string id)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _notificationService.Remove(id);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        return Ok();
    }
}