using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IBasketRepository : IRepository<Basket>
{
    Task<Basket?> GetByCustomerId(string userId);
    Task<bool> HasProduct(string userId, string productId);
}