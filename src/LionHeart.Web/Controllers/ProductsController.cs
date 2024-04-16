using LionHeart.Core.Models;
using LionHeart.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LionHeart.Web.Models.Products;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Dtos.Product;
using LionHeart.Core.Dtos.ProductUnit;

namespace LionHeart.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly IProductUnitService _productUnitService;
    private readonly IFeedbackService _feedbackService;
    private readonly UserManager<User> _userManager;

    public ProductsController(IProductService productService,
                              IProductUnitService productUnitService,
                              IFeedbackService feedbackService,
                              UserManager<User> userManager)
    {
        _productService = productService;
        _productUnitService = productUnitService;
        _feedbackService = feedbackService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var productServiceResult = await _productService.GetProductsWithPagination(pageNumber);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessage);
        var pagedResponse = productServiceResult.Data;
        if (pagedResponse is null) return BadRequest();
        return View(pagedResponse);
    }

    [HttpGet]
    public async Task<IActionResult> ShowProduct(string id, bool showFeedbacks = false)
    {
        var userId = _userManager.GetUserId(User);

        var productServiceResult = await _productService.GetById(id);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessage);

        var product = productServiceResult.Data;
        if (product is null) return BadRequest();

        bool writeFeedback = false;
        if (userId is not null)
        {
            var feedbackServiceResult = await _feedbackService.HasFeedbackPending(userId, product.Id);
            if (feedbackServiceResult.IsFaulted) return BadRequest(feedbackServiceResult.ErrorMessage);
            writeFeedback = feedbackServiceResult.Data;
        }
        var model = new ShowProductViewModel()
        {
            Id = id,
            Price = product.Price,
            Name = product.Name,
            Description = product.Description,
            Specifications = product.Specifications,
            ImageName = product.Image.FileName,
            FeedbackQuantity = product.Feedbacks.Count,
            TotalRating = product.Feedbacks.Count > 0 ? product.Feedbacks.Average(f => (int)f.Rating) : -1,
            ShowFeedbacks = showFeedbacks,
            WriteFeedback = writeFeedback,
            IsDeleted = product.IsDeleted,
            IsInStock = product.Units.Count > 0,
            HasFeedbacks = product.Feedbacks.Count > 0
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
        if (!ModelState.IsValid)
        {
            var userId = _userManager.GetUserId(User);
            if (userId is null) return Unauthorized();

            var dto = new AddProductDto
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                UserId = userId,
                Price = model.Price,
                Description = model.Description,
                Specifications = model.Specifications,
                CreatedAt = DateTimeOffset.UtcNow,
                Image = model.Image
            };
            var productResult = await _productService.Add(dto);
            if (productResult.IsFaulted) return BadRequest(productResult.ErrorMessage);

            var product = productResult.Data;
            if (product is null) return BadRequest();

            var productUnits = new List<AddProductUnitDto>();
            for (int i = 0; i < model.Quantity; i++)
            {
                productUnits.Add(new AddProductUnitDto
                {
                    ProductId = product.Id,
                    CreatedAt = product.CreatedAt,
                    SaleStatus = SaleStatus.Available
                });
            }
            var productUnitResult = await _productUnitService.AddRange(productUnits);
            if (productUnitResult.IsFaulted) return BadRequest(productUnitResult.ErrorMessage);
        }
        return Redirect("/SupplierPanel");
    }

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> EditProduct(string productId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _productService.GetById(productId);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        var product = result.Data;
        if (product is null) return BadRequest();

        var model = new EditProductViewModel()
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Specifications = product.Specifications,
            Quantity = (await _productUnitService.Count(product.Id)).Data
        };
        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> EditProduct(EditProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = new UpdateProductDto
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Specifications = model.Specifications,
                Image = model.Image,
            };
            var result = await _productService.Update(dto);
            if (result.IsFaulted) return BadRequest(result.ErrorMessage);

            return RedirectToAction("ListSupplierProducts");
        }
        return RedirectToAction("EditProduct", "Products", model.Id);
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> DeleteProduct(string productId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _productService.Remove(productId);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

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
        if (!ModelState.IsValid) return View();

        var productServiceResult = await _productService.Search(model.Name, model.PageNumber);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessage);
        var page = productServiceResult.Data;
        if (page is null) return BadRequest();

        ViewData["Page"] = page;
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> ListSupplierProducts()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); 

        var result = await _productService.GetProductsByUserId(userId);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        var products = result.Data;
        if (products is null) return BadRequest();

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
                Quantity = (await _productUnitService.Count(product.Id)).Data
            });
        }
        return View(models);
    }
}