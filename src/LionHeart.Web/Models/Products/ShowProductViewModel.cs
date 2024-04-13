using LionHeart.Core.Enums;

namespace LionHeart.Web.Models.Products;

public class ShowProductViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public string? ImageName { get; set; }
    public bool ShowFeedbacks { get; set; }
    public bool WriteFeedback { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsInStock { get; set; }
    public bool HasFeedbacks { get; set; }
}