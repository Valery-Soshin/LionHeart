using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Services;

public interface IFavoriteProductService
{
    Task<FavoriteProduct?> GetById(string id);
    Task<FavoriteProduct?> GetByUserIdProductId(string userId, string productId);
    Task<List<FavoriteProduct>> GetAll();
    Task<List<FavoriteProduct>> GetAllByUserId(string userId);
    Task Add(FavoriteProduct favoriteProduct);
    Task Update(FavoriteProduct favoriteProduct);
    Task Remove(FavoriteProduct favoriteProduct);
    Task<bool> Any(string userId);
    Task<bool> Any(string userId, string productId);
}