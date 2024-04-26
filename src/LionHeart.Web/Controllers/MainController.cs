using LionHeart.Web.Models.Error;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LionHeart.Web.Controllers;

public class MainController : Controller
{
    public ViewResult Warning(IEnumerable<string> messages, bool returnUrl = false) 
    {
        var logger = HttpContext.RequestServices.GetRequiredService<ILogger<MainController>>();
        logger.LogWarning(string.Join(" ", messages));

        if (returnUrl)
        {
            return View("Information", new InformationViewModel(messages, HttpContext.Request.GetDisplayUrl()));
        }

        return View("Information", new InformationViewModel(messages, null));
    }
    public ViewResult Warning(ModelStateDictionary modelState, bool returnUrl = false)
    {
        var messages = modelState.SelectMany(m => m.Value!.Errors.Select(e => e.ErrorMessage)).ToList();

        var logger = HttpContext.RequestServices.GetRequiredService<ILogger<MainController>>();
        logger.LogWarning(string.Join(" ", messages));

        if (returnUrl)
        {
            return View("Information", new InformationViewModel(messages, HttpContext.Request.GetDisplayUrl()));
        }

        return View("Information", new InformationViewModel(messages, null));
    }
    public ViewResult Error()
    {
        var logger = HttpContext.RequestServices.GetRequiredService<ILogger<MainController>>();
        logger.LogWarning("{path}", HttpContext.Request.Path);

        return View("Error");
    }
}