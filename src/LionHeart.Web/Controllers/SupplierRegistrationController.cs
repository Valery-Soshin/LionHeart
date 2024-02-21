using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class SupplierRegistrationController : Controller
{
    public IActionResult ShowRegistrationInfo()
    {
        return View();
    }
}