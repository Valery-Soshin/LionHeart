using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.BasketEntry;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.BasketEntry;

public class UpdateBasketEntryDtoValidator : AbstractValidator<UpdateBasketEntryDto>
{
    public UpdateBasketEntryDtoValidator()
    {
        RuleFor(d => new IdModel(d.Id))
            .SetValidator(new IdValidator());

        RuleFor(d => d.Quantity)
            .NotEmpty();
    }
}