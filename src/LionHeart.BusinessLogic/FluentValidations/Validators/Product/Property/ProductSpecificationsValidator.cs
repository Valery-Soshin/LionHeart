using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Product.Property;

public class ProductSpecificationsValidator : AbstractValidator<ProductSpecificationsModel>
{
    public ProductSpecificationsValidator()
    {
        RuleFor(p => p.Specifications)
            .NotEmpty()
            .MinimumLength(ModelPropertyConstraints.ProductSpecificationsMinLength)
            .MaximumLength(ModelPropertyConstraints.ProductSpecificationsMaxLength);
    }
}