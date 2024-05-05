using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Category.Property;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.Category;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Category;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(d => new IdModel(d.Id))
            .SetValidator(new IdValidator());

        RuleFor(d => new CategoryNameModel(d.Name))
            .SetValidator(new CategoryNameValidator());
    }
}