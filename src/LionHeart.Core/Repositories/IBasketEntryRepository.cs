using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IBasketEntryRepository : IRepository<BasketEntry>
{
    Task<BasketEntry?> GetByUserProduct(string userId, string productId);
    Task<List<BasketEntry>> GetEntriesByUserId(string userId);
}