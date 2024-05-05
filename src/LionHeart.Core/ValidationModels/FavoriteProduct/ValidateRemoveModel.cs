namespace LionHeart.Core.ValidationModels.FavoriteProduct;

public class ValidateRemoveModel
{
    public required bool FavoriteProductExist { get; init; }
    public required bool UserExists { get; init; }
    public required bool ProductExist { get; init; }
}