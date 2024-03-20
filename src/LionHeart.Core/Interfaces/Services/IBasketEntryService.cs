using LionHeart.Core.Models;
using LionHeart.Core.Response;

namespace LionHeart.Core.Interfaces.Services;

public interface IBasketEntryService
{
    Task<IBaseResponse<BasketEntry>> GetById(string id);
    Task<BasketEntry> GetByUserIdProductId(string userId, string productId);
    Task<List<BasketEntry>> GetEntriesByUserId(string userId);
    Task<int> Add(BasketEntry entry);
    Task<int> Update(BasketEntry entry);
    Task<int> Remove(BasketEntry entry);
    Task<int> Remove(string id);
}