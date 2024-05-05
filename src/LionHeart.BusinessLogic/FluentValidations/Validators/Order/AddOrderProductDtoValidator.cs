using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.Order;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Order;

public class AddOrderProductDtoValidator : AbstractValidator<AddOrderProductDto>
{
    public AddOrderProductDtoValidator()
    {
        RuleFor(d => new IdModel(d.EntryId))
            .SetValidator(new IdValidator());

        RuleFor(d => new IdModel(d.ProductId))
            .SetValidator(new IdValidator());

        RuleFor(d => d.ProductQuantity)
            .NotEmpty();

        RuleFor(d => d.ProductTotalPrice)
            .NotEmpty();
    }
}