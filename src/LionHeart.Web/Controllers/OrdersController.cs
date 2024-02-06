using LionHeart.Core.Models;
using LionHeart.Core.Services;
using LionHeart.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

[Authorize(Roles = "Customer")]
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IProductUnitService _productUnitService;

    public OrdersController(IOrderService orderService,
                           IProductService productService,
                           IProductUnitService productUnitService)
    {
        _orderService = orderService;
        _productService = productService;
        _productUnitService = productUnitService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Basket basket)
    {
        foreach (var product in basket.Products)
        {
            var units = await _productUnitService.GetByProductId(product.ProductId, product.Quantity);

            if (units.Count < product.Quantity)
            {
                return NotFound($"Необходимое количество товара \"{product.Product.Name}\" не доступно.");
            }

            var order = new Order
            {
                ProductId = product.ProductId,
                UserId = product.UserId,
                TotalPrice = product.ProductsTotalPrice,
                Quantity = product.Quantity,
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