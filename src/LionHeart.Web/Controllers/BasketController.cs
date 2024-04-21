using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;
using LionHeart.Web.Models.Basket;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Dtos.Order;
using LionHeart.Core.Dtos.BasketEntry;

namespace LionHeart.Web.Controllers;

[Authorize]
public class BasketController : Controller
{
    private readonly IBasketEntryService _basketEntryService;
    private readonly IOrderService _orderService;
    private readonly UserManager<User> _userManager;
    
    public BasketController(IBasketEntryService basketEntryService,
                            IOrderService orderService,
                            UserManager<User> userManager)
    {
        _basketEntryService = basketEntryService;
        _orderService = orderService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var basketEntryServiceResult = await _basketEntryService.GetEntriesByUserId(userId);
        if (basketEntryServiceResult.IsFaulted) return BadRequest(basketEntryServiceResult.ErrorMessages);
        var entries = basketEntryServiceResult.Value;

        var basket = new BasketViewModel()
        {
            UserId = userId,
            BasketTotalPrice = entries.Sum(e => e.Product.Price * e.Quantity),
            Entries = entries.Select(entry => new BasketEntryViewModel
            {
                Id = entry.Id,
                UserId = entry.UserId,
                ProductId = entry.ProductId,
                ProductName = entry.Product.Name,
                ProductPrice = entry.Product.Price,
                ProductQuantity = entry.Quantity,
                ProductTotalPrice = entry.Product.Price * entry.Quantity,
                ImageName = entry.Product.Image.FileName
            }).ToList()
        };
        return View(basket);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(BasketViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var addOrderDto = new AddOrderDto()
        {
            UserId = model.UserId,
            BasketTotalPrice = model.BasketTotalPrice,
            Products = model.Entries.Select(e => new AddOrderProductDto
            {
                EntryId = e.Id,
                ProductId = e.ProductId,
                ProductQuantity = e.ProductQuantity,
                ProductTotalPrice = e.ProductTotalPrice,
            }).ToList(),
            CreateAt = DateTimeOffset.UtcNow
        };
        var orderServiceResult = await _orderService.Add(addOrderDto);
        if (orderServiceResult.IsFaulted) return BadRequest(orderServiceResult.ErrorMessages);

        return Redirect("/Products");
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket([FromBody] string productId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var dto = new AddBasketEntryDto()
        {
            UserId = userId,
            ProductId = productId,
        };
        var basketEntryServiceResult = await _basketEntryService.Add(dto);
        if (basketEntryServiceResult.IsFaulted) return BadRequest(basketEntryServiceResult.ErrorMessages);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromBasket([FromBody] string productId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var basketEntryServiceResult = await _basketEntryService.GetByAlternateKey(userId, productId);
        if (basketEntryServiceResult.IsFaulted) return BadRequest(basketEntryServiceResult.ErrorMessages);
        var entry = basketEntryServiceResult.Value;

        basketEntryServiceResult = await _basketEntryService.Remove(entry.Id);
        if (basketEntryServiceResult.IsFaulted) return BadRequest(basketEntryServiceResult.ErrorMessages);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var dto = new UpdateBasketEntryDto
        {
            Id = model.EntryId,
            Quantity = model.ProductQuantity
        };
        var basketEntryServiceResult = await _basketEntryService.Update(dto);
        if (basketEntryServiceResult.IsFaulted) return BadRequest(basketEntryServiceResult.ErrorMessages);

        return Ok();
    }
}