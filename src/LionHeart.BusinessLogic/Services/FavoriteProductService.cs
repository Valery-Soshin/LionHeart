using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;

namespace LionHeart.BusinessLogic.Services;

public class FavoriteProductService : IFavoriteProductService
{
    private readonly IFavoriteProductRepository _repository;

    public FavoriteProductService(IFavoriteProductRepository repository)
    {
        _repository = repository;
    }

    public Task<FavoriteProduct?> GetById(string id)
    {
        return _repository.GetById(id);
    }
    public Task<FavoriteProduct?> GetByUserIdProductId(string userId, string productId)
    {
        return _repository.GetByUserIdProductId(userId, productId);
    }
    public Task<List<FavoriteProduct>> GetAll()
    {
        return _repository.GetAll();
    }
    public Task<List<FavoriteProduct>> GetAllByUserId(string userId)
    {
        return _repository.GetAllByUserId(userId);
    }
    public Task Add(FavoriteProduct favoriteProduct)
    {
        return _repository.Add(favoriteProduct);
    }
    public Task Update(FavoriteProduct favoriteProduct)
    {
        return _repository.Update(favoriteProduct);
    }
    public Task Remove(FavoriteProduct favoriteProduct)
    {
        return _repository.Remove(favoriteProduct);
    }
    public Task<bool> Any(string userId)
    {
        return _repository.Any(userId);
    }
    public Task<bool> Any(string userId, string productId)
    {
        return _repository.Any(userId, productId);
    }
}