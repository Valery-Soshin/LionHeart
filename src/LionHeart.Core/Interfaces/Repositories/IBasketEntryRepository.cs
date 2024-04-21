using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IBasketEntryRepository : IRepository<BasketEntry>
{
    Task<BasketEntry?> GetByUserIdProductId(string userId, string productId);
    Task<List<BasketEntry>> GetEntriesByUserId(string userId);
    Task<List<BasketEntry>> Find(List<string> ids);
    Task<bool> Exists(string userId, string productId);
}