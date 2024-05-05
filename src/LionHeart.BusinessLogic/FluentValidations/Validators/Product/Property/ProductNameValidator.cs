using FluentValidation;
using LionHeart.BusinessLogic.Resources;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Product.Property;

public class ProductNameValidator : AbstractValidator<ProductNameModel>
{
    public ProductNameValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MinimumLength(ModelPropertyConstraints.ProductNameMinLength)
            .MaximumLength(ModelPropertyConstraints.ProductNameMaxLength)
            .WithName(PropertyName.ProductName);

        RuleSet("FirstUpperSymbol", () =>
        {
            RuleFor(m => m.Name)
                .Must(p => char.ToUpper(p[0]) + p[1..] == p)
                .WithMessage(ValidationMessage.ProductNameFirstCharNotUpper);
        });
    }
}