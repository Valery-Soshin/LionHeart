using LionHeart.Core.Enums;

namespace LionHeart.Core.Dtos.Feedback;

public class AddFeedbackDto
{
    public string ProductId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public Rating Rating { get; set; }
    public string Content { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}