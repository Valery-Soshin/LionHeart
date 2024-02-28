using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface ICategoryService
{
    Task<Category?> GetById(string id);
    Task<List<Category>> GetAll();
    Task Add(Category category);
    Task Update(Category category);
    Task Remove(Category category);
}