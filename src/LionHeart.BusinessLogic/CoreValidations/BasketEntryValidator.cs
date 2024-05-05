using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.BasketEntry;

namespace LionHeart.BusinessLogic.CoreValidations;

public class BasketEntryValidator : ValidatorBase, IBasketEntryValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (model.BasketEntryAlreadyExists) errorMessages.Add(ErrorMessage.BasketEntryAlreadyExists);
        if (!model.ProductExists) errorMessages.Add(ErrorMessage.ProductNotFound);
        return BuildResult(errorMessages);
    }
    public Result ValidateUpdate(ValidateUpdateModel model)
    {
        var errorMessages = new List<string>();
        if (!model.BasketEntryExists) errorMessages.Add(ErrorMessage.BasketEntryNotFound);
        return BuildResult(errorMessages);
    }
    public Result ValidateRemove(ValidateRemoveModel model)
    {
        var errorMessages = new List<string>();
        if (!model.BasketEntryExists) errorMessages.Add(ErrorMessage.BasketEntryNotFound);
        return BuildResult(errorMessages);
    }
}