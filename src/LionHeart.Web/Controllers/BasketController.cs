using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LionHeart.Core.Services;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace LionHeart.Web.Controllers;
                                                   
[Authorize(Roles = "Customer")]
public class BasketController : Controller
{
    private readonly IBasketService _basketService;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly UserManager<User> _userManager;

    public BasketController(IBasketService basketService,
                            IProductService productService,
                            IOrderService orderService,
                            UserManager<User> userManager)
    {
        _basketService = basketService;
        _productService = productService;
        _orderService = orderService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        if (userId is null)
        {
            return Redirect("/Auth/Login");
        }

        var basket = await _basketService.GetByCustomerId(userId);

        

        //foreach (var products in basket.Products)
        //{
        //    var model = new ProductInBasket()
        //    {
        //        p                
        //    };
        //    models.Add(model);

        //    if (TempData.ContainsKey($"{model.Product.Id}"))
        //    {
        //        if (int.TryParse(TempData[$"{model.Product.Id}"]?.ToString(), out var result))
        //        {
        //            model.Quantity = result;
        //        }
        //    }
        //    else
        //    {
        //        TempData[$"{model.Product.Id}"] = model.Quantity;
        //    }
        //}
        //TempData.Keep();

        return View(basket);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket(string productId)
    {
		var userId = _userManager.GetUserId(User);

		if (!ModelState.IsValid || userId is null)
        {
            return BadRequest("ID Пользователя или ID Продукта не было передано");
        }
        
		var product = new ProductInBasket()
        {
            CustomerId = userId,
            ProductId = productId,
        };

		var basket = await _basketService.GetByCustomerId(userId);

		if (basket is null)
        {
            basket = new Basket()
            {
                CustomerId = userId,
                Products = { product }
            };

            await _basketService.Add(basket);
        }
        else
        {
            basket.Products.Add(product);
            await _basketService.Update(basket);
        }

        return Redirect("/Products/Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromBasket(string productId)
    {
		var userId = _userManager.GetUserId(User);

		if (!ModelState.IsValid || userId is null)
		{
			return BadRequest("ID Пользователя или ID Продукта не было передано");
		}

        var basket = await _basketService.GetByCustomerId(userId);

        if (basket is null)
        {
            // logging
        }
        else
        {
            var product = basket.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product is null)
            {
                // logging
                return BadRequest("Такой продукт не существует");
            }

            basket.Products.Remove(product);
            await _basketService.Update(basket);
        }


        //if (TempData.ContainsKey(productId))
        //{
        //    TempData.Remove(productId);
        //}

        return Redirect("/Products/Index");
    }

    //[HttpPost]
    //public IActionResult UpdateProductQuantity([FromBody] UpdateProductQuantityViewModel model)
    //{
    //    if (TempData.ContainsKey(model.ProductId))
    //    {
    //        TempData[$"{model.ProductId}"] = model.ProductQuantity;
    //        TempData.Keep();
    //    }

    //    return Ok();
    //}
}