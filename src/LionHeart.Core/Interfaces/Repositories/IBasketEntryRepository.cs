using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IBasketEntryRepository : IRepository<BasketEntry>
{
    Task<BasketEntry?> GetByUserIdProductId(string userId, string productId);
    Task<List<BasketEntry>> GetEntriesByUserId(string userId);
}