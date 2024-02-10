using LionHeart.Core.Models;
using LionHeart.Core.Services;
using LionHeart.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LionHeart.Web.Models.Orders;
using Microsoft.AspNetCore.Identity;

namespace LionHeart.Web.Controllers;

[Authorize(Roles = "Customer")]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IProductUnitService _productUnitService;
    private readonly UserManager<User> _userManager;

    public OrdersController(IOrderService orderService,
                           IProductService productService,
                           IProductUnitService productUnitService, 
                           UserManager<User> userManager)
    {
        _orderService = orderService;
        _productService = productService;
        _productUnitService = productUnitService;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
    {
        var userId = _userManager.GetUserId(User);

        foreach (var entry in model.Entries)
        {
            var product = await _productService.GetById(entry.ProductId);
            var units = await _productUnitService.GetByProductId(entry.ProductId, entry.ProductQuantity);

            if (product is null)
            {
                // logging
                continue;
            }

            if (units.Count < entry.ProductQuantity)
            {
                continue;
            }

            var order = new Order
            {
                ProductId = product.Id,
                UserId = userId,
                TotalPrice = product.Price * entry.ProductQuantity,
                Quantity = entry.ProductQuantity,
                CreateAt = DateTimeOffset.UtcNow
            };

            foreach (var unit in units)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductUnitId = unit.Id
                };

                order.OrderDetails.Add(orderDetail);
                unit.SaleStatus = SaleStatus.Sold;
                await _productUnitService.Update(unit);
            }

            await _orderService.Add(order);
            return Redirect("/Products/Index");
        }

        return Redirect("/Basket/Index");
    }  
}