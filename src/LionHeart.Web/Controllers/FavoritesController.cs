using LionHeart.Core.Dtos.FavoriteProduct;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

[Authorize]
public class FavoritesController : Controller
{
    private readonly IFavoriteProductService _favoriteProductService;
    private readonly UserManager<User> _userManager;

    public FavoritesController(IFavoriteProductService favoriteProductService,
                               UserManager<User> userManager)
    {
        _favoriteProductService = favoriteProductService;
        _userManager = userManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToFavorites([FromBody]string productId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); 

        var dto = new AddFavoriteProductDto
        {
            UserId = userId,
            ProductId = productId
        };
        var result = await _favoriteProductService.Add(dto);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromFavorites([FromBody] string productId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized(); 

        var dto = new RemoveFavoriteProductDto
        {
            UserId = userId,
            ProductId = productId
        };
        var result = await _favoriteProductService.Remove(dto);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        return Ok();
    }
}