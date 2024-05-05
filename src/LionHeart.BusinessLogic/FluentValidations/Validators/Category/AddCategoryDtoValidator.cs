using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Category.Property;
using LionHeart.Core.Dtos.Category;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Category;

public class AddCategoryDtoValidator : AbstractValidator<AddCategoryDto>
{
    public AddCategoryDtoValidator()
    {
        RuleFor(d => new CategoryNameModel(d.Name))
            .SetValidator(new CategoryNameValidator());
    }
}