namespace LionHeart.Core.Models;

public class Basket
{
	public string Id { get; set;  } = null!;
	public string CustomerId { get; set; } = null!;
	public decimal BasketTotalPrice { get; set; }
	public List<ProductInBasket> Products { get; set; } = [];
}