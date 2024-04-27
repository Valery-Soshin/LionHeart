using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class ErrorController : MainController
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("Error")]
    [HttpGet]
    [AllowAnonymous]
    public new IActionResult Error()
    {
        var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        _logger.LogError($"The Path {feature?.Path} | Threw an Exception {feature?.Error}");

        return View();
    }
}