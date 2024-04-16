using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Products;

public class SearchProductsViewModel
{
    public string Name { get; set; } = null!;
    public int PageNumber { get; set; } = 1;
}