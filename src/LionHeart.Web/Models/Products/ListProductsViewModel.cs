namespace LionHeart.Web.Models.Products;

public class ListProductsViewModel
{
    public List<ListProductsItemViewModel> Products { get; } = [];
}
public class ListProductsItemViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int FeedbackQuantity { get; set; }
    public double TotalRating { get; set; }
    public string ImageName { get; set; } = null!;
    public bool IsInBasket { get; set; }
    public bool IsInFavorites { get; set; }
    public bool IsDeleted { get; set; }
}