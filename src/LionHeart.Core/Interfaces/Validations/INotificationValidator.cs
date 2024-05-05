using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Notification;

namespace LionHeart.Core.Interfaces.Validations;

public interface INotificationValidator
{
    Result ValidateAdd(ValidateAddModel model);
}