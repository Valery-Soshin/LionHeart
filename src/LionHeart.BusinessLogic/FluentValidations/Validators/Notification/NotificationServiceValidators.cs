using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.Notification;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Notification;

public class NotificationServiceValidators
{
    public INotificationValidator NotificationValidator { get; }
    public IValidator<AddNotificationDto> AddNotificationDtoValidator { get; }
    public IValidator<IdModel> IdValidator { get; }

    public NotificationServiceValidators(INotificationValidator notificationValidator,
                                         IValidator<AddNotificationDto> addNotificationDtoValidator,
                                         IValidator<IdModel> idValidator)
    {
        NotificationValidator = notificationValidator;
        AddNotificationDtoValidator = addNotificationDtoValidator;
        IdValidator = idValidator;
    }
}