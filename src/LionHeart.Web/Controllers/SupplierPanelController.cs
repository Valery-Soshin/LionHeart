using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class SupplierPanelController : MainController
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}