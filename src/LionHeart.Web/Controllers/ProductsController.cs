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
    private readonly IBasketService _markedProductService;
    private readonly UserManager<User> _userManager;

    public ProductsController(IProductService productService,
                              IBasketService markedProductService,
                              UserManager<User> userManager)
    {
        _productService = productService;
        _markedProductService = markedProductService;
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAll();

        var userId = _userManager.GetUserId(User);

        var productsInBasket = await _markedProductService
            .GetAllByCustomerId(userId ?? "", Mark.InBasket) ?? [];

        var models = new List<IndexViewModel>();

        foreach (var product in products)
        {
            models.Add(new IndexViewModel()
            {
                Product = product,
                IsInBasket = productsInBasket.Exists(p => p.ProductId == product.Id)
            });
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