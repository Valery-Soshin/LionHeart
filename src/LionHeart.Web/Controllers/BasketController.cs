using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LionHeart.Core.Services;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;
using LionHeart.Web.Models.Basket;

namespace LionHeart.Web.Controllers;

[Authorize]
public class BasketController : Controller
{
    private readonly IBasketEntryService _basketEntryService;
    private readonly UserManager<User> _userManager;
    
    public BasketController(IBasketEntryService basketEntryService,
                            UserManager<User> userManager)
    {
        _basketEntryService = basketEntryService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var entries = await _basketEntryService.GetEntriesByUserId(userId);

        var basket = new BasketViewModel()
        {
            BasketTotalPrice = entries.Sum(e => e.Product.Price * e.Quantity),
        };
        foreach (var entry in entries)
        {
            basket.Entries.Add(new BasketEntryViewModel
            {
                Id = entry.Id,
                UserId = entry.UserId,
                ProductId = entry.ProductId,
                ProductName = entry.Product.Name,
                ProductPrice = entry.Product.Price,
                ProductQuantity = entry.Quantity,
                ProductTotalPrice = entry.Product.Price * entry.Quantity,
                ImageName = entry.Product.Image.FileName
            });
        }
        return View(basket);
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket([FromBody] string productId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        if (!ModelState.IsValid) return BadRequest();

        await _basketEntryService.Add(new BasketEntry()
        {
            UserId = userId,
            ProductId = productId,
        });
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromBasket([FromBody] string productId)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        if (!ModelState.IsValid) return BadRequest();

        var entry = await _basketEntryService.GetByUserProduct(userId, productId);
        if (entry is null) return NotFound();

        await _basketEntryService.Remove(entry);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var entry = await _basketEntryService.GetById(model.EntryId);
        if (entry is null) return NotFound();

        entry.Quantity = model.ProductQuantity;
        await _basketEntryService.Update(entry);
        return Ok();
    }
}