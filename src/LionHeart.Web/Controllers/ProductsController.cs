using LionHeart.Core.Models;
using LionHeart.Core.Services;
using LionHeart.Web.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
    {
        var supplierId = _userManager.GetUserId(User);
        if (supplierId is null) return BadRequest("SupplierId is NULL");

        var product = new Product
        {
            Name = model.Name,
            CategoryId = model.CategoryId,
            UserId = supplierId,
            Price = model.Price,
            Description = model.Description,
            Specifications = model.Specifications
        };

        await _productService.Add(product);

        if (model.Quantity > 0)
        {
            var createdAt = DateTimeOffset.UtcNow;
            for (int i = 0; i < model.Quantity; i++)
            {
                product.Units.Add(new ProductUnit
                {
                    ProductId = product.Id,
                    CreatedAt = createdAt,
                    SaleStatus = Core.Enums.SaleStatus.Available
                });
            }
            await _productService.Update(product);
        }

        return Redirect("/SupplierPanel");
    }
}