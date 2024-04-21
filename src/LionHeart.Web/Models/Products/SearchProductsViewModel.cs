using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Products;

public class SearchProductsViewModel
{
    public string SearchedValue { get; set; } = null!;
    public PagedResponse<Product> Page { get; set; } = null!;
}