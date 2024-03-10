using LionHeart.Core.Models;
using LionHeart.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

[Authorize]
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); 

        var favoriteProducts = await _favoriteProductService.GetAllByUserId(userId);
        return View(favoriteProducts);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToFavorites([FromBody]string productId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); 

        var product = await _productService.GetById(productId);
        if (product is null) return NotFound(); 

        await _favoriteProductService.Add(new FavoriteProduct()
        {
            ProductId = productId,
            UserId = userId
        });
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromFavorites([FromBody] string productId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); 

        var product = await _productService.GetById(productId);
        if (product is null) return NotFound(); 

        var favoriteProduct = await _favoriteProductService.GetByUserIdProductId(userId, productId);
        if (favoriteProduct is null) return NotFound(); 

        await _favoriteProductService.Remove(favoriteProduct);
        return Ok();
    }
}