using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Feedback;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Feedback;

public class AddFeedbackDtoValidator : AbstractValidator<AddFeedbackDto>
{
    public AddFeedbackDtoValidator()
    {
        RuleFor(d => new IdModel(d.ProductId))
            .SetValidator(new IdValidator());

        RuleFor(d => new IdModel(d.UserId))
            .SetValidator(new IdValidator());

        RuleFor(d => d.Rating)
            .NotEmpty()
            .IsInEnum()
            .WithName(PropertyName.FeedbackRating);

        RuleFor(d => d.Content)
            .NotEmpty()
            .MinimumLength(ModelPropertyConstraints.FeedbackContentMinLength)
            .MaximumLength(ModelPropertyConstraints.FeedbackContentMaxLength)
            .WithName(PropertyName.FeedbackContent);

        RuleFor(d => new DateTimeOffsetModel(d.CreatedAt))
            .SetValidator(new DateTimeOffsetValidator());
    }
}