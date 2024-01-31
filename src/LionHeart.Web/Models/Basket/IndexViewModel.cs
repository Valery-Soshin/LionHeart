using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Basket;

public class IndexViewModel
{
    public List<MarkedProduct> ProductsInBasket { get; set; } = new();
}