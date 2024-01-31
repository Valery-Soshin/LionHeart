using LionHeart.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAll();

        return View(products);
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