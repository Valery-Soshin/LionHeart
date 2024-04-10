namespace LionHeart.Core.Dtos.Notification;

public class AddNotificationDto
{
    public string UserId { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? ProductId { get; set; }
    public string? LinkToAction { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}