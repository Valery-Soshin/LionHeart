namespace LionHeart.Core.ValidationModels.Product;

public class ValidateUpdateModel
{
    public required bool ProductExists { get; init; }
    public required bool CategoryExists { get; init; }
    public required bool BrandExists { get; init; }
}