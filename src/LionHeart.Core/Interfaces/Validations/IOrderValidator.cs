using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Order;

namespace LionHeart.Core.Interfaces.Validations;

public interface IOrderValidator
{
    Result ValidateAdd(ValidateAddModel model);
}