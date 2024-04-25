using LionHeart.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LionHeart.Web.Models.Product;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Dtos.Product;

namespace LionHeart.Web.Controllers;

public class ProductsController : MainController
{
    private readonly IProductService _productService;
    private readonly IProductUnitService _productUnitService;
    private readonly IFeedbackService _feedbackService;
    private readonly IBasketEntryService _basketEntryService;
    private readonly IFavoriteProductService _favoriteProductService;
    private readonly ICompanyService _companyService;
    private readonly UserManager<User> _userManager;

    public ProductsController(IProductService productService,
                              IProductUnitService productUnitService,
                              IFeedbackService feedbackService,
                              IBasketEntryService basketEntryService,
                              IFavoriteProductService favoriteProductService,
                              ICompanyService companyService,
                              UserManager<User> userManager)
    {
        _productService = productService;
        _productUnitService = productUnitService;
        _feedbackService = feedbackService;
        _basketEntryService = basketEntryService;
        _favoriteProductService = favoriteProductService;
        _companyService = companyService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var productServiceResult = await _productService.GetAll(pageNumber);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);
        var page = productServiceResult.Value;

        return View(page);
    }

    [HttpGet]
    public async Task<IActionResult> ShowProduct(string id, int feedbackPageNumber = 1)
    {
        var userId = _userManager.GetUserId(User);

        var productServiceResult = await _productService.GetById(id);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);
        var product = productServiceResult.Value;

        bool writeFeedback = false;
        bool isInBasket = false;
        bool isInFavorites = false;
        if (userId is not null)
        {
            var feedbackServiceResult = await _feedbackService.HasFeedbackPending(userId, product.Id);
            if (feedbackServiceResult.IsFaulted) return BadRequest(feedbackServiceResult.ErrorMessages);
            writeFeedback = feedbackServiceResult.Value;

            var basketEntryServiceResult = await _basketEntryService.Exists(userId, product.Id);
            if (basketEntryServiceResult.IsFaulted) return BadRequest(basketEntryServiceResult.ErrorMessages);
            isInBasket = basketEntryServiceResult.Value;

            var favoriteProductServiceResult = await _favoriteProductService.Exists(userId, product.Id);
            if (favoriteProductServiceResult.IsFaulted) return BadRequest(favoriteProductServiceResult.ErrorMessages);
            isInFavorites = favoriteProductServiceResult.Value;
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
            WriteFeedback = writeFeedback,
            IsDeleted = product.IsDeleted,
            IsInStock = product.Units.Count > 0,
            IsInBasket = isInBasket,
            IsInFavorites = isInFavorites,
            HasFeedbacks = product.Feedbacks.Count > 0,
            Company = new ShowProductCompanyViewModel
            {
                Id = product.Company.Id,
                Name = product.Company.Name,
            },
            Brand = new ShowProductBrandViewModel
            {
                Id = product.Brand.Id,
                Name = product.Brand.Name
            },
            FeedbackPageNumber = feedbackPageNumber
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
        if (ModelState.IsValid)
        {
            var userId = _userManager.GetUserId(User);
            if (userId is null) return Unauthorized();

            var companyServiceResult = await _companyService.GetByUserId(userId);
            if (companyServiceResult.IsFaulted) return BadRequest(companyServiceResult.ErrorMessages);
            var company = companyServiceResult.Value;

            var dto = new AddProductDto
            {
                UserId = userId,
                Name = model.Name,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                CompanyId = company.Id,
                Price = model.Price,
                Description = model.Description,
                Specifications = model.Specifications,
                Quantity = model.Quantity,
                CreatedAt = DateTimeOffset.UtcNow,
                Image = model.Image
            };
            var productServiceResult = await _productService.Add(dto);
            if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);
            return Redirect("/SupplierPanel");
        }
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> EditProduct(string productId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var productServiceResult = await _productService.GetById(productId);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);
        var product = productServiceResult.Value;

        var model = new EditProductViewModel()
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Specifications = product.Specifications,
            Quantity = (await _productUnitService.Count(product.Id)).Value
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
            var productServiceResult = await _productService.Update(dto);
            if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);

            return RedirectToAction("ListSupplierProducts");
        }
        return RedirectToAction("EditProduct", "Products", model.Id);
    }

    [HttpPost]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> DeleteProduct(string productId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var productServiceResult = await _productService.Remove(productId);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);

        return RedirectToAction("ListSupplierProducts");
    }

    [HttpGet]
    public IActionResult SearchProducts()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SearchProducts(string searchedValue, int pageNumber = 1)
    {
        if (!ModelState.IsValid) return View();

        var productServiceResult = await _productService.Search(searchedValue, pageNumber);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);
        var page = productServiceResult.Value;

        if (page.Entities.Count == 0)
        {
            ModelState.AddModelError("", "Товары не найдены");
            return View();
        }
        var model = new SearchProductsViewModel()
        {
            SearchedValue = searchedValue,
            Page = page
        };
        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> ListSupplierProducts(int pageNumber = 1)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); 

        var productServiceResult = await _productService.GetProductsByUserId(userId, pageNumber);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);
        var page = productServiceResult.Value;

        var model = new ListSupplierProductsViewModel()
        {
            PageNumber = page.PageNumber,
            HasPreviousPage = page.HasPreviousPage,
            HasNextPage = page.HasNextPage
        };
        foreach (var product in page.Entities)
        {
            model.Products.Add(new SupplierProductViewModel()
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Specifications = product.Specifications,
                CreatedAt = product.CreatedAt,
                Quantity = (await _productUnitService.Count(product.Id)).Value
            });
        }
        return View(model);
    }
}