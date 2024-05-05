using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Brand;

namespace LionHeart.Core.Interfaces.Validations;

public interface IBrandValidator
{
    Result ValidateAdd(ValidateAddModel model);
}