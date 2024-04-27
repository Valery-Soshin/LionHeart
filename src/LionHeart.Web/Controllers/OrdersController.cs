using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Models.Order;
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

        var orderServiceResult = await _orderService.GetOrderItemsByUserId(userId, pageNumber);
        if (orderServiceResult.IsFaulted) return Warning(orderServiceResult.ErrorMessages);
        var page = orderServiceResult.Value;

        var model = new IndexViewModel
        {
            PageNumber = page.PageNumber,
            HasPreviousPage = page.HasPreviousPage,
            HasNextPage = page.HasNextPage,
            Orders = page.Entities.Select(i => new IndexOrderViewModel()
            {
                ProductName = i.Product.Name,
                ProductPrice = i.ProductPrice,
                ProductQuantity = i.ProductQuantity,
                CreatedAt = i.Order.CreatedAt
            }).ToList()
        };
        return View(model);
    }
}