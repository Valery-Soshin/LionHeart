using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.ProductUnit;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.ProductUnit;

public class AddProductUnitValidator : AbstractValidator<AddProductUnitDto>
{
    public AddProductUnitValidator()
    {
        RuleFor(d => new IdModel(d.ProductId))
            .SetValidator(new IdValidator());

        RuleFor(d => d.SaleStatus)
            .IsInEnum();

        RuleFor(d => new DateTimeOffsetModel(d.CreatedAt))
            .SetValidator(new DateTimeOffsetValidator());
    }
}