using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;
using LionHeart.Web.Models.Basket;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Dtos.Orders;
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

        var result = await _basketEntryService.GetEntriesByUserId(userId);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        var entries = result.Data;
        if (entries is null) return BadRequest();

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
                ProductId = e.ProductId,
                ProductQuantity = e.ProductQuantity,
                ProductTotalPrice = e.ProductTotalPrice
            }).ToList(),
            CreateAt = DateTimeOffset.UtcNow
        };
        var orderResult = await _orderService.Add(addOrderDto);
        if (orderResult.IsFaulted) return BadRequest(orderResult.ErrorMessage);

        var removeBasketEntryDtos = model.Entries.Select(e => new RemoveBasketEntryDto { Id = e.Id }).ToList();
        var basketEntryResult = await _basketEntryService.RemoveRange(removeBasketEntryDtos);
        if (basketEntryResult.IsFaulted) return BadRequest(basketEntryResult.ErrorMessage);

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
        var result = await _basketEntryService.Add(dto);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromBasket([FromBody] string productId)
    {
        if (!ModelState.IsValid) return BadRequest();

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var result = await _basketEntryService.GetByUserIdProductId(userId, productId);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        var dto = new RemoveBasketEntryDto()
        {
            Id = result!.Data!.Id
        };
        result = await _basketEntryService.Remove(dto);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

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
        var result = await _basketEntryService.Update(dto);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        return Ok();
    }
}