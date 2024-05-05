using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Product.Property;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.Product;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Product;

public class AddProductDtoValidator : AbstractValidator<AddProductDto>
{
    public AddProductDtoValidator()
    {
        RuleFor(d => new ProductNameModel(d.Name))
            .SetValidator(new ProductNameValidator(), ruleSets: "FirstUpperSymbol");

        RuleFor(d => new IdModel(d.CategoryId))
            .SetValidator(new IdValidator());

        RuleFor(d => new IdModel(d.BrandId))
            .SetValidator(new IdValidator());

        RuleFor(d => new IdModel(d.CompanyId))
            .SetValidator(new IdValidator());

        RuleFor(d => new ProductPriceModel(d.Price))
            .SetValidator(new ProductPriceValidator());

        RuleFor(d => new ProductDescriptionModel(d.Description))
            .SetValidator(new ProductDescriptionValidator());

        RuleFor(d => new ProductSpecificationsModel(d.Specifications))
            .SetValidator(new ProductSpecificationsValidator());

        RuleFor(d => d.Quantity)
            .NotEmpty();

        RuleFor(d => new DateTimeOffsetModel(d.CreatedAt))
            .SetValidator(new DateTimeOffsetValidator());

        RuleFor(d => d.Image)
            .NotNull();
    }
}