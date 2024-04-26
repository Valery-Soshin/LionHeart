using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class OrdersController : MainController
{
    private readonly IOrderService _orderService;
    private readonly UserManager<User> _userManager;

    public OrdersController(IOrderService orderService,
                            UserManager<User> userManager)
    {
        _orderService = orderService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        if (!ModelState.IsValid) return Warning(ModelState);

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var orderServiceResult = await _orderService.GetOrdersByUserId(userId, pageNumber);
        if (orderServiceResult.IsFaulted) return Warning(orderServiceResult.ErrorMessages);
        var page = orderServiceResult.Value;

        return View(page.Entities);
    }
}