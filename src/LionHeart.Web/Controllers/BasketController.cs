using Microsoft.AspNetCore.Mvc;
using LionHeart.Web.Models.Basket;

namespace LionHeart.Web.Controllers;

public class BasketController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string userId)
    {
        var model = new IndexViewModel();
        return View(model);
    }
}