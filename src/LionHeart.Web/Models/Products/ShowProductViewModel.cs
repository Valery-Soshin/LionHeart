namespace LionHeart.Web.Models.Products;

public class ShowProductViewModel
{
    public string Id { get; set; } = null!;
    public ShowProductCompanyViewModel Company { get; set; } = null!;
    public ShowProductBrandViewModel Brand { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public string? ImageName { get; set; }
    public int FeedbackQuantity { get; set; }
    public double TotalRating { get; set; }
    public bool WriteFeedback { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsInStock { get; set; }
    public bool IsInBasket { get; set; }
    public bool IsInFavorites { get; set; }
    public bool HasFeedbacks { get; set; }
}
public class ShowProductCompanyViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}
public class ShowProductBrandViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}