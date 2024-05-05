using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Category.Property;

public class CategoryNameValidator : AbstractValidator<CategoryNameModel>
{
    public CategoryNameValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MinimumLength(ModelPropertyConstraints.CategoryNameMinLength)
            .MaximumLength(ModelPropertyConstraints.CategoryNameMaxLength)
            .WithName(PropertyName.CategoryName);
    }
}