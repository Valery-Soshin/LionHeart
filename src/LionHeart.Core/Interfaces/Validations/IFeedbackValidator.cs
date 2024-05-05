using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Feedback;

namespace LionHeart.Core.Interfaces.Validations;

public interface IFeedbackValidator
{
    Result ValidateAdd(ValidateAddModel model);
    Result ValidateRemove(ValidateRemoveModel model);
}