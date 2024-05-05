using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Product.Property;

public class ProductPriceValidator : AbstractValidator<ProductPriceModel>
{
    public ProductPriceValidator()
    {
        RuleFor(p => p.Price)
            .NotEmpty()
            .LessThan(ModelPropertyConstraints.ProductMaxPrice);
    }
}