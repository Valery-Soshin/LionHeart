using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}