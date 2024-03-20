using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Models.Orders;
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var response = await _orderService.GetOrdersByUserId(userId);
        if (response.IsFaulted)
        {
            return BadRequest(response.Description);
        }
        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        //var createOrderModel = new CreateOrderDto()
        //{
        //    BasketTotalPrice = model.BasketTotalPrice,
        //    Entries = model.Entries,
        //    UserId = userId
        //};
        //var response = await _orderService.Add(model);
        //if (response.IsFaulted)
        //{
        //    return BadRequest(response.Description);
        //}
        return Redirect("/Products");
    }
}