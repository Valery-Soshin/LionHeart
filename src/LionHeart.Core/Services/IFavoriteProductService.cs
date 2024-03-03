using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IFavoriteProductService
{
    Task<FavoriteProduct?> GetById(string id);
    Task<List<FavoriteProduct>> GetAll();
    Task Add(FavoriteProduct favoriteProduct);
    Task Update(FavoriteProduct favoriteProduct);
    Task Remove(FavoriteProduct favoriteProduct);
    Task<bool> Any(string userId);
    Task<bool> Any(string userId, string productId);
}