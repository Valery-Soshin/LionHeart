using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Product;

public class SearchProductsViewModel
{
    public string SearchedValue { get; set; } = null!;
    public PagedResponse<Core.Models.Product> Page { get; set; } = null!;
}