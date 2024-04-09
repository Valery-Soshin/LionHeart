using LionHeart.Core.Dtos.Category;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface ICategoryService
{
    Task<Result<Category>> GetById(string id);
    Task<Result<List<Category>>> GetAll();
    Task<Result<List<Category>>> GetParentCategories();
    Task<Result<Category>> Add(AddCategoryDto dto);
    Task<Result<Category>> Update(UpdateCategoryDto dto);
    Task<Result<Category>> Remove(string id);
}