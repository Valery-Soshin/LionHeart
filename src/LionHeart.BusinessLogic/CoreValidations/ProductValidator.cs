using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Product;

namespace LionHeart.BusinessLogic.CoreValidations;

public class ProductValidator : ValidatorBase, IProductValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (model.ProductAlreadyExists) errorMessages.Add(ErrorMessage.ProductNameAlreadyExists);
        if (!model.CategoryExists) errorMessages.Add(ErrorMessage.CategoryNotFound);
        if (!model.BrandExists) errorMessages.Add(ErrorMessage.BrandNotFound);
        if (!model.CompanyExists) errorMessages.Add(ErrorMessage.CompanyNotFound);
        return BuildResult(errorMessages);
    }
    public Result ValidateUpdate(ValidateUpdateModel model)
    {
        var errorMessages = new List<string>();
        if (!model.ProductExists) errorMessages.Add(ErrorMessage.ProductNotFound);
        if (!model.CategoryExists) errorMessages.Add(ErrorMessage.CategoryNotFound);
        if (!model.BrandExists) errorMessages.Add(ErrorMessage.BrandNotFound);
        return BuildResult(errorMessages);
    }
    public Result ValidateRemove(ValidateRemoveModel model)
    {
        var errorMessages = new List<string>();
        if (!model.ProductExists) errorMessages.Add(ErrorMessage.ProductNotFound);
        return BuildResult(errorMessages);
    }
}