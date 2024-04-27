using LionHeart.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class SupplierPanelController : MainController
{
    [HttpGet]
    [Authorize(Roles = RoleNameHelper.Supplier)]
    public IActionResult Index()
    {
        return View();
    }
}