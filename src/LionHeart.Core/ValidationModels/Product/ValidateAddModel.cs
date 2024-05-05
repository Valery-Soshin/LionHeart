namespace LionHeart.Core.ValidationModels.Product;

public class ValidateAddModel()
{
    public required bool ProductAlreadyExists { get; init; }
    public required bool CategoryExists { get; init; }
    public required bool BrandExists { get; init; }
    public required bool CompanyExists { get; init; }
}