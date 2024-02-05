using LionHeart.Core.Enums;
using LionHeart.Core.Models;
using LionHeart.Core.Services;
using LionHeart.Web.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IBasketService _basketService;
    private readonly UserManager<User> _userManager;

    public ProductsController(IProductService productService,
                              IBasketService basketService,
                              UserManager<User> userManager)
    {
        _productService = productService;
        _basketService = basketService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        var products = await _productService.GetAll();
        var models = new List<IndexViewModel>();

        if (userId is not null)
        {
            var basket = await _basketService.GetByCustomerId(userId);

            foreach (var product in products)
            {
                models.Add(new IndexViewModel()
                {
                    Product = product,
                    IsInBasket = basket.Products.Exists(p => p.ProductId == product.Id)
                });
            }
        }
        else
        {
            foreach (var product in products)
            {
                models.Add(new IndexViewModel()
                {
                    Product = product,
                    IsInBasket = false
                });
            }
        }

        return View(models);
    }

    [HttpGet]
    public async Task<IActionResult> Show(string id, bool showFeedbacks = false)
    {
        var product = await _productService.GetById(id);

        if (product is null)
        {
            return View("Error");
        }

        ViewBag.ShowFeedbacks = showFeedbacks;

        return View(product);
    }
}