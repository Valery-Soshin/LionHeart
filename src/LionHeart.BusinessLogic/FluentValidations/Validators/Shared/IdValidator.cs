using FluentValidation;
using LionHeart.BusinessLogic.Resources;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Shared;

public class IdValidator : AbstractValidator<IdModel>
{
    public IdValidator()
    {
        RuleForEach(m => m.Ids)
            .NotEmpty()
            .Length(ModelPropertyConstraints.IdLength)
            .WithName(PropertyName.Id);
    }
}