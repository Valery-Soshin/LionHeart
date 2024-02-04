using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IBasketRepository : IRepository<Basket>
{
    Task<Basket?> GetByCustomerId(string customerId);
    Task<bool> HasProduct(string customerId, string productId);
}