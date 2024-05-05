using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.Notification;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Notification;

public class AddNotificationDtoValidator : AbstractValidator<AddNotificationDto>
{
    public AddNotificationDtoValidator()
    {
        RuleFor(d => new IdModel(d.UserId))
            .SetValidator(new IdValidator());

        RuleFor(d => d.Content)
            .NotEmpty();

        RuleFor(d => new DateTimeOffsetModel(d.CreatedAt))
            .SetValidator(new DateTimeOffsetValidator());
    }
}