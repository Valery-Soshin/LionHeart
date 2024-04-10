namespace LionHeart.Core.Models;

public class Notification
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? LinkToAction { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}