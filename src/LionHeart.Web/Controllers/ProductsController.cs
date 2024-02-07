using LionHeart.Core.Models;
using LionHeart.Core.Services;
using LionHeart.Web.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IBasketEntryService _basketEntryService;
    private readonly UserManager<User> _userManager;

    public ProductsController(IProductService productService,
                              IBasketEntryService basketEntryService,
                              UserManager<User> userManager)
    {
        _productService = productService;
        _basketEntryService = basketEntryService;
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
            var entries = await _basketEntryService.GetEntriesByUserId(userId);

            foreach (var product in products)
            {
                models.Add(new IndexViewModel()
                {
                    Product = product,
                    IsInBasket = entries.Exists(e => e.ProductId == product.Id)
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