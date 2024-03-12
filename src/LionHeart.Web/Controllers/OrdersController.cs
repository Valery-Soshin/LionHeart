using LionHeart.Core.Enums;
using LionHeart.Core.Models;
using LionHeart.Core.Services;
using LionHeart.Web.Models.Basket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IBasketEntryService _basketEntryService;
    private readonly UserManager<User> _userManager;

    public OrdersController(IOrderService orderService,
                            IProductService productService,
                            IBasketEntryService basketEntryService,
                            UserManager<User> userManager)
    {
        _orderService = orderService;
        _productService = productService;
        _basketEntryService = basketEntryService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var orders = await _orderService.GetOrdersByUserId(userId);
        return View(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(BasketViewModel basket)
    {
        if (!ModelState.IsValid) return BadRequest($"Значение BasketTotalPrice: {basket.BasketTotalPrice}");

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var order = new Order
        {
            UserId = userId,
            TotalPrice = basket.BasketTotalPrice,
            CreateAt = DateTimeOffset.UtcNow
        };

        foreach (var entry in basket.Entries)
        {
            var product = await _productService.GetById(entry.ProductId);
            if (product is null) continue;
            if (product.Units.Count < entry.ProductQuantity) continue;

            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = product.Id,
                ProductPrice = product.Price,
                ProductQuantity = entry.ProductQuantity
            };
            order.Items.Add(orderItem);

            for (int i = 0; i < entry.ProductQuantity; i++)
            {
                var unit = product.Units[i];

                orderItem.Details.Add(new OrderItemDetail
                {
                    OrderItemId = order.Id,
                    ProductUnitId = unit.Id
                });
                unit.SaleStatus = SaleStatus.Sold;
            }

            await _productService.Update(product);
            await _basketEntryService.Remove(entry.Id);
        }
        await _orderService.Add(order);
        return Redirect("/Products");
    }
}