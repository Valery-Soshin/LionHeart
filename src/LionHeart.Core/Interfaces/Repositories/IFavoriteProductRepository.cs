using LionHeart.Core.Models;
using System.Linq.Expressions;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IFavoriteProductRepository : IRepository<FavoriteProduct>
{
    Task<FavoriteProduct?> GetByAlternateKey(string userId, string productId);
    Task<PagedResponse<FavoriteProduct>> GetFavoritesByUserId(int pageNumber, int pageSize, Expression<Func<FavoriteProduct, bool>> filter);
    Task<bool> Any(string userId);
    Task<bool> Exists(string userId, string productId);
}