namespace LionHeart.Web.Models.Profile;

public class ShowNotificationsViewModel
{
    public List<ShowNotificationItemViewModel> Notifications { get; set; } = [];
}
public class ShowNotificationItemViewModel
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? LinkToAction { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}