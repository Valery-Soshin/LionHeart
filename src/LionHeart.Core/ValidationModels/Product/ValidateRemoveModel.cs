namespace LionHeart.Core.ValidationModels.Product;

public class ValidateRemoveModel(bool productExists)
{
    public bool ProductExists { get; } = productExists;
}