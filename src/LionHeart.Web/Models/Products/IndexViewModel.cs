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
    public string FileName { get; set; } = null!;
    public bool IsInBasket { get; set; }
    public bool IsInFavorites { get; set; }
}