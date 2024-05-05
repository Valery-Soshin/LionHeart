using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Category;

namespace LionHeart.Core.Interfaces.Validations;

public interface ICategoryValidator
{
    Result ValidateAdd(ValidateAddModel model);
    Result ValidateUpdate(ValidateUpdateModel model);
    Result ValidateRemove(ValidateRemoveModel model);
}