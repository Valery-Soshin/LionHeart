using Microsoft.AspNetCore.Mvc;
using LionHeart.Web.Models.Basket;
using Microsoft.AspNetCore.Authorization;
using LionHeart.Core.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace LionHeart.Web.Controllers;
                                                   
[Authorize(Roles = "Customer")]
public class BasketController : Controller
{
    private readonly IMarkedProductService _markedProductService;
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;

    public BasketController(IMarkedProductService markedProductService,
                            IProductService productService,
                            UserManager<User> userManager)
    {
        _markedProductService = markedProductService;
        _productService = productService;
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

        var markedProducts = await _markedProductService
            .GetAllByCustomerId(userId, Mark.InBasket);

        var models = new List<IndexViewModel>();

        foreach (var markedProduct in markedProducts)
        {
            var model = new IndexViewModel()
            {
                Product = markedProduct.Product,
                Quantity = 1
            };
            models.Add(model);

            if (TempData.ContainsKey($"{model.Product.Id}"))
            {
                if (int.TryParse(TempData[$"{model.Product.Id}"]?.ToString(), out var result))
                {
                    model.Quantity = result;
                }
            }
            else
            {
                TempData[$"{model.Product.Id}"] = model.Quantity;
            }
        }

        TempData.Keep();
        return View(models);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket(string userId, string productId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("ID Пользователя или ID Продукта не было передано");
        }

        var markedProduct = new MarkedProduct()
        {
            CustomerId = userId,
            ProductId = productId,
            Mark = Mark.InBasket
        };

        await _markedProductService.Add(markedProduct);

        return Redirect("/Products/Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromBasket(string userId, string productId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("ID Пользователя или ID Продукта не было передано");
        }

        var markedProduct = await _markedProductService
            .GetByCustomerIdProductId(userId, productId, Mark.InBasket);

        if (markedProduct is null)
        {
            return BadRequest("Такая отметка о выбранном продукте не существует!");
        }

        await _markedProductService.Remove(markedProduct);
        if (TempData.ContainsKey(productId))
        {
            TempData.Remove(productId);
        }

        return Redirect("/Products/Index");
    }

    [HttpPost]
    public IActionResult UpdateProductQuantity([FromBody] UpdateProductQuantityViewModel model)
    {
        if (TempData.ContainsKey(model.ProductId))
        {
            TempData[$"{model.ProductId}"] = model.ProductQuantity;
            TempData.Keep();
        }

        return Ok();
    }

    public record class UpdateProductQuantityViewModel(string ProductId, int ProductQuantity);
}