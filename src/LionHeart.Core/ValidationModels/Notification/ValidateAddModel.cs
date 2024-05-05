namespace LionHeart.Core.ValidationModels.Notification;

public class ValidateAddModel
{
    public required bool NotificationAlreadyExists { get; init; }
    public required bool UserExists { get; init; }
}