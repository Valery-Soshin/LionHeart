using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.BasketEntry;

namespace LionHeart.Core.Interfaces.Validations;

public interface IBasketEntryValidator
{
    Result ValidateAdd(ValidateAddModel model);
    Result ValidateUpdate(ValidateUpdateModel model);
    Result ValidateRemove(ValidateRemoveModel model);
}