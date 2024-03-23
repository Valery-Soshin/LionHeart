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
                return new Result<Category>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CategoryNotFound
                };
            }
            return new Result<Category>
            {
                IsCompleted = true,
                Data = category
            };
        }
        catch
        {
            return new Result<Category>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<Category>>> GetAll()
    {
        try
        {
            var categories = await _categoryRepository.GetAll();
            if (categories is null)
            {
                return new Result<List<Category>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CategoriesNotFound
                };
            }
            return new Result<List<Category>>
            {
                IsCompleted = true,
                Data = categories
            };
        }
        catch
        {
            return new Result<List<Category>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
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
                return new Result<Category>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CategoryNotCreated
                };
            }
            return new Result<Category>
            {
                IsCompleted = true,
                Data = category
            };
        }
        catch
        {
            return new Result<Category>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Category>> Update(UpdateCategoryDto dto)
    {
        try
        {
            var category = await _categoryRepository.GetById(dto.Id);
            if (category is null)
            {
                return new Result<Category>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CategoryNotFound
                };
            }

            category.Name = dto.Name;

            var result = await _categoryRepository.Update(category);
            if (result <= 0)
            {
                return new Result<Category>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CategoryNotUpdated
                };
            }
            return new Result<Category>
            {
                IsCompleted = true,
                Data = category
            };
        }
        catch
        {
            return new Result<Category>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Category>> Remove(RemoveCategoryDto dto)
    {
        try
        {
            var category = await _categoryRepository.GetById(dto.Id);
            if (category is null)
            {
                return new Result<Category>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CategoryNotFound
                };
            }
            var result = await _categoryRepository.Remove(category);
            if (result <= 0)
            {
                return new Result<Category>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CategoryNotRemoved
                };
            }
            return new Result<Category>
            {
                IsCompleted = true,
                Data = category
            };
        }
        catch
        {
            return new Result<Category>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}