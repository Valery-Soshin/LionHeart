namespace LionHeart.Web.Models.Products;

public class SearchProductsViewModel
{
    public string SearchedValue { get; set; } = null!;
    public int PageNumber { get; set; } = 1;
}