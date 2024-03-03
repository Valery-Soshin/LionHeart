using LionHeart.Core.Models;
using LionHeart.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class FavoritesController : Controller
{
    private readonly IFavoriteProductService _favoriteProductService;
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;

    public FavoritesController(IFavoriteProductService favoriteProductService,
                               IProductService productService,
                               UserManager<User> userManager)
    {
        _favoriteProductService = favoriteProductService;
        _productService = productService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize(Roles = "Customer, Supplier")]
    public async Task<IActionResult> AddToFavorites([FromBody]string productId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); // logging

        var product = await _productService.GetById(productId);
        if (product is null) return BadRequest(); // logging

        var favoriteProduct = new FavoriteProduct()
        {
            ProductId = productId,
            UserId = userId
        };

        await _favoriteProductService.Add(favoriteProduct);

        return Ok();
    }
}