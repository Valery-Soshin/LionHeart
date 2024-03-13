using LionHeart.Core.Enums;

namespace LionHeart.Web.Models.Feedbacks;

public class CreateFeedbackViewModel
{
    public string ProductId { get; set; } = null!;
    public Rating Rating { get; set; }
    public string Content { get; set; } = null!;
}