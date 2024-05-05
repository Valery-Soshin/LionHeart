using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Brand;

namespace LionHeart.BusinessLogic.CoreValidations;

public class BrandValidator : ValidatorBase, IBrandValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (model.BrandAlreadyExists) errorMessages.Add(ErrorMessage.BrandAlreadyExists);
        return BuildResult(errorMessages);
    }
}