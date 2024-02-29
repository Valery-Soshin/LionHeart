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
    private readonly IProductUnitService _productUnitService;
    private readonly IBasketEntryService _basketEntryService;
    private readonly UserManager<User> _userManager;

    public ProductsController(IProductService productService,
                              IProductUnitService productUnitService,
                              IBasketEntryService basketEntryService,
                              UserManager<User> userManager)
    {
        _productService = productService;
        _productUnitService = productUnitService;
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
            Specifications = model.Specifications,
            CreatedAt = DateTimeOffset.UtcNow
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

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> EditProduct(string productId)
    {
        var product = await _productService.GetById(productId);

        if (product is null) return NotFound(); // logging

        var model = new SupplierProductViewModel()
        {
            Product = product,
            Quantity = await _productUnitService.CountByProductId(productId)
        };

        return View(model);
    }
    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> EditProduct(SupplierProductViewModel model)
    {
        var product = await _productService.GetById(model.Product.Id);

        if (product is null) return NotFound(); // logging

        product.Name = model.Product.Name;
        product.Price = model.Product.Price;
        product.CategoryId = model.Product.CategoryId;
        product.Description = model.Product.Description ?? string.Empty;
        product.Specifications = model.Product.Specifications ?? string.Empty;

        await _productService.Update(product);

        return RedirectToAction("ListSupplierProducts");
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> DeleteProduct(string productId)
    {
        // сделать мягкое удаление

        var product = await _productService.GetById(productId);
        if (product is null) return NotFound(); // logging

        await _productService.Remove(product);

        return RedirectToAction("ListSupplierProducts");
    }

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> ListSupplierProducts()
    {
        var supplierId = _userManager.GetUserId(User);
        if (supplierId is null) return BadRequest(); // logging

        var products = await _productService.GetProductsByUserId(supplierId);

        var models = new List<SupplierProductViewModel>();

        foreach (var product in products)
        {
            models.Add(new SupplierProductViewModel()
            {
                Product = product,
                Quantity = await _productUnitService.CountByProductId(product.Id)
            });
        }

        return View(models);
    }
}