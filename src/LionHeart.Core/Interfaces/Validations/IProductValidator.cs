using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Product;

namespace LionHeart.Core.Interfaces.Validations;

public interface IProductValidator
{
    Result ValidateAdd(ValidateAddModel model);
    Result ValidateUpdate(ValidateUpdateModel model);
    Result ValidateRemove(ValidateRemoveModel model);
}