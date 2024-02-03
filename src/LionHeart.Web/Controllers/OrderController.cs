using LionHeart.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

[Authorize(Roles = "Customer")]
public class OrderController : Controller
{


    public OrderController()
    {
        
    }

    [HttpPost]
    public async Task<IActionResult> Process([FromBody]List<OrderViewModel> models)
        {
        return Ok("Заказ оформлен!");
    }

    public record class OrderViewModel(Product Product, int Quantity);
}