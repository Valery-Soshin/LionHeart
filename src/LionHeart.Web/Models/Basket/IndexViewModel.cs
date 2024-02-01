using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Basket;

public class IndexViewModel
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
}