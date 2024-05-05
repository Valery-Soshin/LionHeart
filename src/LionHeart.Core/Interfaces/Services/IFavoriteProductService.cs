using LionHeart.Core.Models;
using LionHeart.Core.Results;

namespace LionHeart.Core.Interfaces.Services;

public interface IFavoriteProductService
{
    Task<Result<FavoriteProduct>> GetById(string id);
    Task<Result<FavoriteProduct>> GetByAlternateKey(string userId, string productId);
    Task<Result<PagedResponse<FavoriteProduct>>> GetFavoritesByUserId(string userId, int pageNumber);
    Task<Result<FavoriteProduct>> Add(string userId, string productId);
    Task<Result<FavoriteProduct>> Remove(string userId, string productId);
    Task<Result<bool>> Any(string userId);
    Task<Result<bool>> Exists(string userId, string productId);
}