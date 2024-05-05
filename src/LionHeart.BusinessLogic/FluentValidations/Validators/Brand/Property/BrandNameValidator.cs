using FluentValidation;
using LionHeart.BusinessLogic.Resources;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Brand.Property;

public class BrandNameValidator : AbstractValidator<BrandNameModel>
{
    public BrandNameValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MinimumLength(ModelPropertyConstraints.BrandNameMinLength)
            .MaximumLength(ModelPropertyConstraints.BrandNameMaxLength)
            .WithName(PropertyName.BrandName);
    }
}