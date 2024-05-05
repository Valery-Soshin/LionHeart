using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.Order;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Order;

public class AddOrderDtoValidator : AbstractValidator<AddOrderDto>
{
    public AddOrderDtoValidator()
    {
        RuleFor(d => new IdModel(d.UserId))
            .SetValidator(new IdValidator());

        RuleFor(d => d.BasketTotalPrice)
            .NotEmpty();

        RuleForEach(d => d.Products)
            .SetValidator(new AddOrderProductDtoValidator());

        RuleFor(d => new DateTimeOffsetModel(d.CreateAt))
            .SetValidator(new DateTimeOffsetValidator());
    }
}