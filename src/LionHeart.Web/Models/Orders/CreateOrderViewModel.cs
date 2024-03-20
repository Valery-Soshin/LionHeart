using LionHeart.Web.Models.Basket;

namespace LionHeart.Web.Models.Orders;

public class CreateOrderViewModel
{
    public string UserId { get; set; } = null!;
    public decimal BasketTotalPrice { get; set; }
    public List<BasketEntryViewModel> Entries { get; set; } = [];
}