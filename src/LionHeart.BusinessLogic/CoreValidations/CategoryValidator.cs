using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Category;

namespace LionHeart.BusinessLogic.CoreValidations;

public class CategoryValidator : ValidatorBase, ICategoryValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (model.CategoryAlreadyExists) errorMessages.Add(ErrorMessage.CategoryAlreadyExists);
        return BuildResult(errorMessages);
    }
    public Result ValidateUpdate(ValidateUpdateModel model)
    {
        var errorMessages = new List<string>();
        if (!model.CategoryExists) errorMessages.Add(ErrorMessage.CategoryNotFound);
        return BuildResult(errorMessages);
    }
    public Result ValidateRemove(ValidateRemoveModel model)
    {
        var errorMessages = new List<string>();
        if (!model.CategoryExists) errorMessages.Add(ErrorMessage.CategoryNotFound);
        return BuildResult(errorMessages);
    }
}