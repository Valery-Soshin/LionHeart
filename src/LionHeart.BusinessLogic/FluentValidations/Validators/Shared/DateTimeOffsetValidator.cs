using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.Resources;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Shared;

public class DateTimeOffsetValidator : AbstractValidator<DateTimeOffsetModel>
{
    public DateTimeOffsetValidator()
    {
        RuleFor(m => m.DateTimeOffset)
            .NotEmpty()
            .WithName(PropertyName.DateTimeOffset);
    }
}