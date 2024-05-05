using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Notification;

namespace LionHeart.BusinessLogic.CoreValidations;

public class NotificationValidator : ValidatorBase, INotificationValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (model.NotificationAlreadyExists) errorMessages.Add(ErrorMessage.NotificationAlreadyExists);
        if (!model.UserExists) errorMessages.Add(ErrorMessage.UserNotFound);
        return BuildResult(errorMessages);
    }
}