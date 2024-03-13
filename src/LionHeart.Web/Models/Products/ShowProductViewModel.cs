using LionHeart.Core.Enums;

namespace LionHeart.Web.Models.Products;

public class ShowProductViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public string? ImageName { get; set; }
    public List<FeedbackViewModel> Feedbacks { get; set; } = [];
    public bool ShowFeedbacks { get; set; }
    public bool WriteFeedback { get; set; }
}


public class FeedbackViewModel
{
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public Rating Rating { get; set; }
    public string Content { get; set; } = null!;
}