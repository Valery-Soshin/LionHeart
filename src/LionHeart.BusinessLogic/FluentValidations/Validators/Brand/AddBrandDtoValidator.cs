using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Brand.Property;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.Brand;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Brand;

public class AddBrandDtoValidator : AbstractValidator<AddBrandDto>
{
    public AddBrandDtoValidator()
    {
        RuleFor(d => new BrandNameModel(d.Name))
            .SetValidator(new BrandNameValidator());

        RuleFor(d => new IdModel(d.ImageId))
            .SetValidator(new IdValidator());
    }
}