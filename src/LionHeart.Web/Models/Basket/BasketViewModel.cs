namespace LionHeart.Web.Models.Basket;

public class BasketViewModel
{
    public string UserId { get; set; } = null!;
    public decimal BasketTotalPrice { get; set; }
    public List<BasketEntryViewModel> Entries { get; set; } = [];
}