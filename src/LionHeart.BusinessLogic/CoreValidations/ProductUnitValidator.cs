using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.ProductUnit;

namespace LionHeart.BusinessLogic.CoreValidations;

public class ProductUnitValidator : ValidatorBase, IProductUnitValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (!model.ProductExists) errorMessages.Add(ErrorMessage.ProductNotFound);
        return BuildResult(errorMessages);
    }
    public Result ValidateUpdate(ValidateUpdateModel model)
    {
        var errorMessages = new List<string>();
        if (!model.ProductUnitExists) errorMessages.Add(ErrorMessage.ProductUnitNotFound);
        return BuildResult(errorMessages);
    }
}