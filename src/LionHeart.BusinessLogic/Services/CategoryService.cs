using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Category;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Category;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Category;

namespace LionHeart.BusinessLogic.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryServiceValidators _validators;

    public CategoryService(ICategoryRepository categoryRepository,
                           CategoryServiceValidators validators)
    {
        _categoryRepository = categoryRepository;
        _validators = validators;
    }

    public async Task<Result<Category>> GetById(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Category>.Failure(errorMessages);
            }

            var category = await _categoryRepository.GetById(id);
            if (category is null)
            {
                return Result<Category>.Failure(ErrorMessage.CategoryNotFound);
            }
            return Result<Category>.Success(category);
        }
        catch
        {
            return Result<Category>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<Category>>> GetParentCategories()
    {
        try
        {
            var categories = await _categoryRepository.GetParentCategories();
            if (categories.Count == 0)
            {
                return Result<List<Category>>.Failure(ErrorMessage.CategoriesNotFound);
            }
            return Result<List<Category>>.Success(categories);
        }
        catch
        {
            return Result<List<Category>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Category>> Add(AddCategoryDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.AddCategoryDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Category>.Failure(errorMessages);
            }

            bool categoryAlreadyExists = await _categoryRepository.Exists(c => c.Name == dto.Name);
            var validateAddModel = new ValidateAddModel()
            {
                CategoryAlreadyExists = categoryAlreadyExists
            };
            var categoryValidatorResult = _validators.CategoryValidator.ValidateAdd(validateAddModel);
            if (categoryValidatorResult.IsFaulted)
            {
                return Result<Category>.Failure(categoryValidatorResult.ErrorMessages);
            }

            var category = new Category
            {
                Name = dto.Name
            };
            var result = await _categoryRepository.Add(category);
            if (result <= 0)
            {
                return Result<Category>.Failure(ErrorMessage.CategoryNotCreated);
            }
            return Result<Category>.Success(category);
        }
        catch
        {
            return Result<Category>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Category>> Update(UpdateCategoryDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.UpdateCategoryDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Category>.Failure(errorMessages);
            }

            bool categoryExists = await _categoryRepository.Exists(c => c.Id == dto.Id);
            var validateUpdateModel = new ValidateUpdateModel()
            {
                CategoryExists = categoryExists
            };
            var categoryValidatorResult = _validators.CategoryValidator.ValidateUpdate(validateUpdateModel);        
            if (categoryValidatorResult.IsFaulted)
            {
                return Result<Category>.Failure(categoryValidatorResult.ErrorMessages);
            }

            var category = await _categoryRepository.GetById(dto.Id);
            if (category is null)
            {
                return Result<Category>.Failure(ErrorMessage.CategoryNotFound);
            }
            category.Name = dto.Name;
            var categoryRepositoryResult = await _categoryRepository.Update(category);
            if (categoryRepositoryResult <= 0)
            {
                return Result<Category>.Failure(ErrorMessage.CategoryNotUpdated);
            }
            return Result<Category>.Success(category);
        }
        catch
        {
            return Result<Category>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Category>> Remove(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Category>.Failure(errorMessages);
            }

            bool categoryExists = await _categoryRepository.Exists(c => c.Id == id);
            var validateRemoveModel = new ValidateRemoveModel()
            {
                CategoryExists = categoryExists
            };
            var categoryValidatorResult = _validators.CategoryValidator.ValidateRemove(validateRemoveModel);
            if (categoryValidatorResult.IsFaulted)
            {
                return Result<Category>.Failure(categoryValidatorResult.ErrorMessages);
            }

            var category = await _categoryRepository.GetById(id);
            if (category is null)
            {
                return Result<Category>.Failure(ErrorMessage.CategoryNotFound);
            }
            var categoryRepositoryResult = await _categoryRepository.Remove(category);
            if (categoryRepositoryResult <= 0)
            {
                return Result<Category>.Failure(ErrorMessage.CategoryNotRemoved);
            }
            return Result<Category>.Success(category);
        }
        catch
        {
            return Result<Category>.Failure(ErrorMessage.InternalServerError);
        }
    }
}