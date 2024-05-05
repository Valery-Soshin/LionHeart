using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Feedback;

namespace LionHeart.BusinessLogic.CoreValidations;

public class FeedbackValidator : ValidatorBase, IFeedbackValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (model.FeedbackAlreadyExists) errorMessages.Add(ErrorMessage.FeedbackAlreadyExists);
        if (!model.UserExists) errorMessages.Add(ErrorMessage.UserNotFound);
        if (!model.ProductExists) errorMessages.Add(ErrorMessage.ProductNotFound);
        return BuildResult(errorMessages);
    }
    public Result ValidateRemove(ValidateRemoveModel model)
    {
        var errorMessages = new List<string>();
        if (!model.FeedbackExists) errorMessages.Add(ErrorMessage.FeedbackNotFound);
        return BuildResult(errorMessages);
    }
}