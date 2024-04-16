using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Models.Products;
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

    public async Task<IViewComponentResult> InvokeAsync(List<Product> products)
    {
        var userId = _userManager.GetUserId(UserClaimsPrincipal);

        var model = new ListProductsViewModel();
        foreach (var product in products)
        {
            bool isInBasket = userId is not null &&
                (await _basketEntryService.Exists(userId, product.Id)).Data;

            bool isInFavorites = userId is not null &&
                (await _favoriteProductService.Exists(userId, product.Id)).Data;

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
        return View(model);
    }
}