using Microsoft.AspNetCore.Mvc;
using LionHeart.Web.Models.Basket;
using Microsoft.AspNetCore.Authorization;

namespace LionHeart.Web.Controllers;

[Authorize(Roles = "Customer")]
public class BasketController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string userId)
    {
        var model = new IndexViewModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket(string productId)
    {
        var product 
    }
}