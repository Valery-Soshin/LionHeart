using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.FavoriteProduct;

namespace LionHeart.Core.Interfaces.Validations;

public interface IFavoriteProductValidator
{
    Result ValidateAdd(ValidateAddModel model);
    Result ValidateRemove(ValidateRemoveModel model);
}