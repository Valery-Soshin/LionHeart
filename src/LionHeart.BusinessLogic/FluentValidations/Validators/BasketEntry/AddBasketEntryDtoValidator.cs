using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.BasketEntry;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.BasketEntry;

public class AddBasketEntryDtoValidator : AbstractValidator<AddBasketEntryDto>
{
    public AddBasketEntryDtoValidator()
    {
        RuleFor(d => new IdModel(d.UserId))
            .SetValidator(new IdValidator());

        RuleFor(d => new IdModel(d.ProductId))
            .SetValidator(new IdValidator());

        RuleFor(d => new DateTimeOffsetModel(d.CreatedAt))
            .SetValidator(new DateTimeOffsetValidator());
    }
}