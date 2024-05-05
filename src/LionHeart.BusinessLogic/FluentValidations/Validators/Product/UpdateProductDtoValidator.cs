using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Product.Property;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.Product;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Product;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(p => new IdModel(p.Id))
            .SetValidator(new IdValidator());

        RuleFor(p => new ProductNameModel(p.Name))
            .SetValidator(new ProductNameValidator());

        RuleFor(p => new IdModel(p.CategoryId))
            .SetValidator(new IdValidator());

        RuleFor(p => new IdModel(p.BrandId))
            .SetValidator(new IdValidator());

        RuleFor(p => new ProductPriceModel(p.Price))
            .SetValidator(new ProductPriceValidator());

        RuleFor(p => new ProductDescriptionModel(p.Description))
            .SetValidator(new ProductDescriptionValidator());

        RuleFor(p => new ProductSpecificationsModel(p.Specifications))
            .SetValidator(new ProductSpecificationsValidator());
    }
}