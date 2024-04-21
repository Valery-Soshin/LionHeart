using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Category;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Category>> GetById(string id)
    {
        try
        {
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
    public async Task<Result<Category>> Add(AddCategoryDto dto)
    {
        try
        {
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
            var category = await _categoryRepository.GetById(dto.Id);
            if (category is null)
            {
                return Result<Category>.Failure(ErrorMessage.CategoryNotFound);
            }
            category.Name = dto.Name;
            var result = await _categoryRepository.Update(category);
            if (result <= 0)
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
            var category = await _categoryRepository.GetById(id);
            if (category is null)
            {
                return Result<Category>.Failure(ErrorMessage.CategoryNotFound);
            }
            var result = await _categoryRepository.Remove(category);
            if (result <= 0)
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
}