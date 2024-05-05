using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.ProductUnit;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.ProductUnit;

public class UpdateProductUnitDtoValidator : AbstractValidator<UpdateProductUnitDto>
{
    public UpdateProductUnitDtoValidator()
    {
        RuleFor(d => new IdModel(d.Id))
            .SetValidator(new IdValidator());

        RuleFor(d => d.SaleStatus)
            .IsInEnum();
    }
}