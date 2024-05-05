namespace LionHeart.Core.ValidationModels.Feedback;

public class ValidateAddModel
{
    public required bool FeedbackAlreadyExists { get; init; }
    public required bool UserExists { get; init; }
    public required bool ProductExists { get; init; }
}