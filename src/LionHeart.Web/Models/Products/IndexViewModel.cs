using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Products;

public class IndexViewModel
{
    public Product Product { get; set; } = null!;
    public bool IsInBasket { get; set; }
}