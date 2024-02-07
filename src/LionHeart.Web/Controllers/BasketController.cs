using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LionHeart.Core.Services;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;
using LionHeart.Web.Models.Basket;

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
	//[HttpPost]
	//public IActionResult Index(Basket basket)
	//{
	//	return View(basket);
	//}

	[HttpPost]
	public async Task<IActionResult> AddToBasket(string productId)
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

		return Redirect("/Products/Index");
	}

	[HttpPost]
	public async Task<IActionResult> RemoveFromBasket(string productId)
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

        return Redirect("/Products/Index");
	}

	[HttpPost]
	public async Task<IActionResult> UpdateBasket(BasketViewModel basket)
	{
		

		return null;
	}
}