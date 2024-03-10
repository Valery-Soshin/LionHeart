using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class SupplierPanelController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}