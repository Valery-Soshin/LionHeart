namespace LionHeart.Core.ValidationModels.FavoriteProduct;

public class ValidateAddModel
{
    public required bool FavoriteProductAlreadyExist { get; init; }
    public required bool UserExists { get; init; }
    public required bool ProductExist { get; init; }
}