using LionHeart.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class ProfilesController : Controller
{

    public ProfilesController()
    {
        
    }

    [HttpGet]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> ShowProfile(string userId)
    {
        var profile = new ProfileViewModel() { Name = "Valery" };

        return View(profile);
    }
}