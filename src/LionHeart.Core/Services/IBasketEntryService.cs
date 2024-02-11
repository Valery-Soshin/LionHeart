using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IBasketEntryService
{
    Task<BasketEntry?> GetById(string id);
    Task<BasketEntry?> GetByUserProduct(string userId, string productId);
    Task<List<BasketEntry>> GetEntriesByUserId(string userId);
    Task<int> Add(BasketEntry entry);
    Task<int> Update(BasketEntry entry);
    Task<int> Remove(BasketEntry entry);
    Task<int> Remove(string id);
}