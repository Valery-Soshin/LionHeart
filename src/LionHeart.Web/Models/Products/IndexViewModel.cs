namespace LionHeart.Web.Models.Products;

public class IndexViewModel
{
    public required int PageNumber { get; init; }
    public required bool HasPreviousPage { get; init; }
    public required bool HasNextPage { get; init; }
    public List<IndexProductViewModel> Products { get; } = [];
}
public class IndexProductViewModel
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