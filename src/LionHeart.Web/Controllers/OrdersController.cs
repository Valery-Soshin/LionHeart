using LionHeart.Core.Models;
using LionHeart.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly UserManager<User> _userManager;

    public OrdersController(IOrderService orderService,
                           UserManager<User> userManager)
    {
        _orderService = orderService;
        _userManager = userManager;
    }

    public async Task<IActionResult> ListOrders()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var orders = await _orderService.GetOrdersByUserId(userId);

        return View(orders);
    }
}