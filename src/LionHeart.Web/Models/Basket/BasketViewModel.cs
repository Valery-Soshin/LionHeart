namespace LionHeart.Web.Models.Basket;

public class BasketViewModel
{
    public decimal BasketTotalPrice { get; set; }
    public List<BasketEntryViewModel> Entries { get; set; } = [];
}