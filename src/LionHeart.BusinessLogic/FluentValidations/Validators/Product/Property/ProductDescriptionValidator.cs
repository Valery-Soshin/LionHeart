using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Product.Property;

public class ProductDescriptionValidator : AbstractValidator<ProductDescriptionModel>
{
    public ProductDescriptionValidator()
    {
        RuleFor(m => m.Description)
            .NotEmpty()
            .MinimumLength(ModelPropertyConstraints.ProductDescriptionMinLength)
            .MaximumLength(ModelPropertyConstraints.ProductDescriptionMaxLength);
    }
}