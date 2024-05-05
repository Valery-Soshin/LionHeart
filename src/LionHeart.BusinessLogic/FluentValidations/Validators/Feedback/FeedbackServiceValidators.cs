using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.Feedback;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Feedback;

public class FeedbackServiceValidators
{
    public IFeedbackValidator FeedbackValidator { get; }
    public IValidator<AddFeedbackDto> AddFeedbackDtoValidator { get; }
    public IValidator<IdModel> IdValidator { get; }

    public FeedbackServiceValidators(IFeedbackValidator feedbackValidator,
                                     IValidator<AddFeedbackDto> addFeedbackDtoValidator,
                                     IValidator<IdModel> idValidator)
    {
        FeedbackValidator = feedbackValidator;
        AddFeedbackDtoValidator = addFeedbackDtoValidator;
        IdValidator = idValidator;
    }
}