using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class BasketEntryService : IBasketEntryService
{
    private readonly IBasketEntryRepository _repository;

    public BasketEntryService(IBasketEntryRepository repository)
    {
        _repository = repository;
    }

    public Task<BasketEntry?> GetById(string id)
    {
        return _repository.GetById(id);
    }
    public Task<BasketEntry?> GetByUserProduct(string userId, string productId)
    {
        return _repository.GetByUserProduct(userId, productId);
    }
    public Task<List<BasketEntry>> GetEntriesByUserId(string userId)
    {
        return _repository.GetEntriesByUserId(userId);
    }
    public Task<int> Add(BasketEntry entry)
    {
        return _repository.Add(entry);
    }
    public Task<int> Update(BasketEntry entry)
    {
        return _repository.Update(entry);
    }
    public Task<int> Remove(BasketEntry entry)
    {
        return _repository.Remove(entry);
    }
    public Task<int> Remove(string id)
    {
        return _repository.Remove(id);
    }
}