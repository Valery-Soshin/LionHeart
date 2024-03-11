namespace LionHeart.Web.Models.Basket;

public class BasketEntryViewModel
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; } = 1;
    public decimal ProductTotalPrice { get; set; }
    public string? ImageName { get; set; } 
}