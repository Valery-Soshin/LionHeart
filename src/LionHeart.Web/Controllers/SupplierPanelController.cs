using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class SupplierPanelController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}