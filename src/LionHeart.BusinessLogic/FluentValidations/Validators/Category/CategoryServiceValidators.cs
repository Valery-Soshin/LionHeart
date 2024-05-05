using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.Category;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Category;

public class CategoryServiceValidators
{
    public ICategoryValidator CategoryValidator { get; }
    public IValidator<AddCategoryDto> AddCategoryDtoValidator { get; }
    public IValidator<UpdateCategoryDto> UpdateCategoryDtoValidator { get; }
    public IValidator<IdModel> IdValidator { get; }

    public CategoryServiceValidators(ICategoryValidator categoryValidator,
                                     IValidator<AddCategoryDto> addCategoryDtoValidator,
                                     IValidator<UpdateCategoryDto> updateCategoryDtoValidator,
                                     IValidator<IdModel> idValidator)
    {
        CategoryValidator = categoryValidator;
        AddCategoryDtoValidator = addCategoryDtoValidator;
        UpdateCategoryDtoValidator = updateCategoryDtoValidator;
        IdValidator = idValidator;
    }
}