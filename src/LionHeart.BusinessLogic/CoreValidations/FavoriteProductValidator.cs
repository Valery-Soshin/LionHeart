using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.FavoriteProduct;

namespace LionHeart.BusinessLogic.CoreValidations;

public class FavoriteProductValidator : ValidatorBase, IFavoriteProductValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (model.FavoriteProductAlreadyExist) errorMessages.Add(ErrorMessage.FavoriteProductAlreadyExists);
        if (!model.UserExists) errorMessages.Add(ErrorMessage.UserNotFound);
        if (!model.ProductExist) errorMessages.Add(ErrorMessage.ProductNotFound);
        return BuildResult(errorMessages);
    }
    public Result ValidateRemove(ValidateRemoveModel model)
    {
        var errorMessages = new List<string>();
        if (!model.FavoriteProductExist) errorMessages.Add(ErrorMessage.FavoriteProductAlreadyExists);
        if (!model.UserExists) errorMessages.Add(ErrorMessage.UserNotFound);
        if (!model.ProductExist) errorMessages.Add(ErrorMessage.ProductNotFound);
        return BuildResult(errorMessages);
    }
}