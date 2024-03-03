using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IFavoriteProductRepository : IRepository<FavoriteProduct>
{
    Task<bool> Any(string userId);
    Task<bool> Any(string userId, string productId);
}