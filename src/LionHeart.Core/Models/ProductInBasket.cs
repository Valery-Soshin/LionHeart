namespace LionHeart.Core.Models;

public class ProductInBasket : MarkedProduct
{
    public int Quantity { get; set; } = 1;
    public int TotalPrice { get; set; }
}