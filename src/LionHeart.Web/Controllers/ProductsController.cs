using LionHeart.Core.Models;
using LionHeart.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LionHeart.Web.Models.Products;
using LionHeart.Core.Interfaces.Services;

namespace LionHeart.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IProductUnitService _productUnitService;
    private readonly IBasketEntryService _basketEntryService;
    private readonly IFavoriteProductService _favoriteProductService;
    private readonly IFeedbackService _feedbackService;
    private readonly UserManager<User> _userManager;

    public ProductsController(IProductService productService,
                              IOrderService orderService,
                              IProductUnitService productUnitService,
                              IBasketEntryService basketEntryService,
                              IFavoriteProductService favoriteProductService,
                              IFeedbackService feedbackService,
                              UserManager<User> userManager)
    {
        _productService = productService;
        _orderService = orderService;
        _productUnitService = productUnitService;
        _basketEntryService = basketEntryService;
        _favoriteProductService = favoriteProductService;
        _feedbackService = feedbackService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(List<string>? ids = null)
    {
        var userId = _userManager.GetUserId(User);
        var products = new List<Product>();

        if (ids is null)
        {
            products = await _productService.GetAll();
        }
        else
        {
            foreach (var id in ids)
            {
                var product = await _productService.GetById(id);
                if (product is not null)
                {
                    products.Add(product);
                }
            }
        }

        var models = new List<IndexViewModel>();

        if (userId is not null)
        {
            var entries = await _basketEntryService.GetEntriesByUserId(userId);
            foreach (var product in products)
            {
                models.Add(new IndexViewModel()
                {
                    Product = product,
                    IsInBasket = entries.Exists(e => e.ProductId == product.Id),
                    IsInFavorites = await _favoriteProductService.Any(userId, product.Id)
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
    public async Task<IActionResult> ShowProduct(string id, bool showFeedbacks = false)
    {
        var userId = _userManager.GetUserId(User);

        var product = await _productService.GetById(id);
        if (product is null) return NotFound();

        bool writeFeedback = userId is not null &&
            await _feedbackService.HasFeedbackPending(userId, product.Id);
        
        var model = new ShowProductViewModel()
        {
            Id = id,
            Name = product.Name,
            Description = product.Description,
            Specifications = product.Specifications,
            ImageName = product.Image.FileName,
            Feedbacks = product.Feedbacks.Select(f => new FeedbackViewModel(){
                FirstName = f.User.FirstName,
                LastName = f.User.LastName,
                Rating = f.Rating,
                Content = f.Content,
                CreatedAt = f.CreatedAt
            }).ToList(),
            ShowFeedbacks = showFeedbacks,
            WriteFeedback = writeFeedback
        };
        return View(model);
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
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var product = new Product
        {
            Name = model.Name,
            CategoryId = model.CategoryId,
            UserId = userId,
            Price = model.Price,
            Description = model.Description,
            Specifications = model.Specifications,
            CreatedAt = DateTimeOffset.UtcNow,
            Image = new ImageModel()
            {
                FileName = model.Image.FileName,
                File = model.Image
            }
        };
        await _productService.Add(product);

        var createdAt = DateTimeOffset.UtcNow;
        for (int i = 0; i < model.Quantity; i++)
        {
            product.Units.Add(new ProductUnit
            {
                ProductId = product.Id,
                CreatedAt = createdAt,
                SaleStatus = SaleStatus.Available
            });
        }
        await _productService.Update(product);
        return Redirect("/SupplierPanel");
    }

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> EditProduct(string productId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var product = await _productService.GetById(productId);
        if (product is null) return NotFound(); 

        var model = new EditProductViewModel()
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Specifications = product.Specifications,
            Quantity = await _productUnitService.CountByProductId(product.Id)
        };
        return View(model);
    }
    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> EditProduct(EditProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = await _productService.GetById(model.Id);
            if (product is null) return NotFound();

            product.Name = model.Name;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.Specifications = model.Specifications;

            await _productService.Update(product);
            return RedirectToAction("ListSupplierProducts");
        }
        return RedirectToAction("EditProduct", "Products", model.Id);
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> DeleteProduct(string productId)
    {
        var product = await _productService.GetById(productId);
        if (product is null) return NotFound(); 

        await _productService.Remove(product);
        return RedirectToAction("ListSupplierProducts");
    }

    [HttpGet]
    public IActionResult SearchProducts()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SearchProducts(SearchProductsViewModel model)
    {
        if (ModelState.IsValid)
        {
            var products = await _productService.Search(model.Name);
            if (products.Count != 0)
            {
                return RedirectToAction("Index", new { ids = products.Select(p => p.Id).ToList() });
            }
        }
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> ListSupplierProducts()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); 

        var products = await _productService.GetProductsByUserId(userId);
        var models = new List<SupplierProductViewModel>();
        foreach (var product in products)
        {
            models.Add(new SupplierProductViewModel()
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Specifications = product.Specifications,
                CreatedAt = product.CreatedAt,
                Quantity = await _productUnitService.CountByProductId(product.Id)
            });
        }
        return View(models);
    }
}