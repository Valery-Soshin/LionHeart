using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Models.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Components;

public class ListProductsViewComponent : ViewComponent
{
    private readonly IProductService _productService;
    private readonly IBasketEntryService _basketEntryService;
    private readonly IFavoriteProductService _favoriteProductService;
    private readonly UserManager<User> _userManager;

    public ListProductsViewComponent(IProductService productService,
                                     IBasketEntryService basketEntryService,
                                     IFavoriteProductService favoriteProductService,
                                     UserManager<User> userManager)
    {
        _productService = productService;
        _basketEntryService = basketEntryService;
        _favoriteProductService = favoriteProductService;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync(PagedResponse<Product> page, bool useButtonsForPage = false)
    {
        var userId = _userManager.GetUserId(UserClaimsPrincipal);

        ViewData["UseButtonsForPage"] = useButtonsForPage;
        
        var model = new ListProductsViewModel()
        {
            PageNumber = page.PageNumber,
            HasPreviousPage = page.HasPreviousPage,
            HasNextPage = page.HasNextPage
        };
        foreach (var product in page.Entities)
        {
            bool isInBasket = userId is not null &&
                (await _basketEntryService.Exists(userId, product.Id)).Value;

            bool isInFavorites = userId is not null &&
                (await _favoriteProductService.Exists(userId, product.Id)).Value;

            model.Products.Add(new ListProductsItemViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FeedbackQuantity = product.Feedbacks.Count,
                TotalRating = product.Feedbacks.Count > 0 ? product.Feedbacks.Average(f => (int)f.Rating) : -1,
                ImageName = product.Image.FileName,
                IsInBasket = isInBasket,
                IsInFavorites = isInFavorites
            });
        }
        return View("/Views/Products/Components/ListProducts/Default.cshtml", model);
    }
}