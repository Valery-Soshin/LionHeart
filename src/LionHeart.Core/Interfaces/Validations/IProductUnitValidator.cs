using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.ProductUnit;

namespace LionHeart.Core.Interfaces.Validations;

public interface IProductUnitValidator
{
    Result ValidateAdd(ValidateAddModel model);
    Result ValidateUpdate(ValidateUpdateModel model);
}