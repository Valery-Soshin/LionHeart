using LionHeart.Web.Models.Error;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class MainController : Controller
{
    public ViewResult Warning(IEnumerable<string> messages, bool returnUrl = false) 
    {
        if (returnUrl)
        {
            return View("Information", new InformationViewModel(messages, HttpContext.Request.GetDisplayUrl()));
        }

        return View("Information", new InformationViewModel(messages, null));
    }
    public ViewResult Error()
    {
        return View("Error");
    }
}