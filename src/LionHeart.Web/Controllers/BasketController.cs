using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LionHeart.Core.Services;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;
using LionHeart.Web.Models.Basket;
using LionHeart.Core.Enums;

namespace LionHeart.Web.Controllers;
												   
[Authorize(Roles = "Customer")]
public class BasketController : Controller
{
	private readonly IBasketEntryService _basketEntryService;
	private readonly IProductService _productService;
	private readonly IOrderService _orderService;
	private readonly UserManager<User> _userManager;

	public BasketController(IBasketEntryService basketEntryService,
							IProductService productService,
                            IOrderService orderService,
							UserManager<User> userManager)
	{
		_basketEntryService = basketEntryService;
		_productService = productService;
		_orderService = orderService;
		_userManager = userManager;
	}

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var userId = _userManager.GetUserId(User);

		if (userId is null)
		{
			return Redirect("/Auth/Login");
		}

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
				ProductsTotalPrice = entry.Product.Price * entry.Quantity,
			});
		}

		return View(basket);
	}

    [HttpPost]
    public async Task<IActionResult> CreateOrder(BasketViewModel basket)
    {
        var userId = _userManager.GetUserId(User);

        if (userId is null) return Unauthorized();

        var order = new Order
        {
            UserId = userId,
            TotalPrice = basket.BasketTotalPrice,
            CreateAt = DateTimeOffset.UtcNow
        };

        foreach (var entry in basket.Entries)
        {
            var product = await _productService.GetById(entry.ProductId);

			if (product is null) continue;
			if (product.Units.Count < entry.ProductQuantity)
				return Content($"Продукт \"{product.Name}\" отсутствует на складе");

			var orderItem = new OrderItem
			{
				OrderId = order.Id,
				ProductId = product.Id,
				ProductPrice = product.Price,
				ProductQuantity = entry.ProductQuantity
			};
			order.Items.Add(orderItem);
			
            for(int i = 0; i < entry.ProductQuantity; i++)
            {
				var unit = product.Units[i];

                orderItem.Details.Add(new OrderItemDetail
                {
                    OrderItemId = order.Id,
                    ProductUnitId = unit.Id
                });
                unit.SaleStatus = SaleStatus.Sold;
            }

            await _productService.Update(product);
			await _basketEntryService.Remove(entry.Id);
        }
        await _orderService.Add(order);

        return Redirect("/Products/Index");
    }

    [HttpPost]
	public async Task<IActionResult> AddToBasket(string productId, string? returnURL = null)
	{
		var userId = _userManager.GetUserId(User);

		if (string.IsNullOrWhiteSpace(productId) || userId is null)
		{
			// logging
			return BadRequest("ID Пользователя или ID Продукта не было передано");
		}
		
		var entry = new BasketEntry()
		{
			UserId = userId,
			ProductId = productId,
		};

		var result = await _basketEntryService.Add(entry);

		if (result is 0)
		{
			// logging
		}

		if (returnURL is not null)
		{
			return Redirect(returnURL);
		}
		return Redirect("/Products/Index");
	}

	[HttpPost]
	public async Task<IActionResult> RemoveFromBasket(string productId, string? returnURL = null)
	{
		var userId = _userManager.GetUserId(User);

		if (string.IsNullOrWhiteSpace(productId) || userId is null)
		{
			// logging
			return BadRequest("ID Пользователя или ID Продукта не было передано");
		}

		var entry = await _basketEntryService.GetByUserProduct(userId, productId);

		if (entry is not null)
		{
			await _basketEntryService.Remove(entry);
		}
		else
		{
			// logging
		}

        if (returnURL is not null)
        {
            return Redirect(returnURL);
        }
        return Redirect("/Products/Index");
    }

    public async Task<IActionResult> UpdateBasket([FromBody]UpdateTempData model)
    {
		var userId = _userManager.GetUserId(User);
		var entry = await _basketEntryService.GetById(model.EntryId);

		if (entry is null || entry.UserId != userId) return BadRequest();

		entry.Quantity = model.ProductQuantity;
		await _basketEntryService.Update(entry);

		return Ok();
	}

	public record class UpdateTempData(string EntryId, int ProductQuantity);
}