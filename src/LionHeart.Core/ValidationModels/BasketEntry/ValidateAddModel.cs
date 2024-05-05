namespace LionHeart.Core.ValidationModels.BasketEntry;

public class ValidateAddModel
{
    public required bool BasketEntryAlreadyExists { get; init; }
    public required bool ProductExists { get; init; }
}