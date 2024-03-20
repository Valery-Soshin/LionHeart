using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;

namespace LionHeart.BusinessLogic.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public Task<Category?> GetById(string id)
    {
        return _repository.GetById(id);
    }
    public Task<List<Category>> GetAll()
    {
        return _repository.GetAll();
    }
    public Task Add(Category category)
    {
        return _repository.Add(category);
    }
    public Task Update(Category category)
    {
        return _repository.Update(category);
    }
    public Task Remove(Category category)
    {
        return _repository.Remove(category);
    }
}