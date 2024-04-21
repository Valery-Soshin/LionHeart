using LionHeart.BusinessLogic.Helpers;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class FavoriteProductService : IFavoriteProductService
{
    private readonly IFavoriteProductRepository _favoriteRepository;

    public FavoriteProductService(IFavoriteProductRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task<Result<FavoriteProduct>> GetById(string id)
    {
        try
        {
            var favoriteProduct = await _favoriteRepository.GetById(id);
            if (favoriteProduct is null) return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotFound);
            
            return Result<FavoriteProduct>.Success(favoriteProduct);
        }
        catch
        {
            return Result<FavoriteProduct>.Failure(ErrorMessage.InternalServerError);
        }
    }                                      
    public async Task<Result<FavoriteProduct>> GetByAlternateKey(string userId, string productId)
    {
        try
        {
            var favoriteProduct = await _favoriteRepository.GetByAlternateKey(userId, productId);
            if (favoriteProduct is null) return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotFound);
            
            return Result<FavoriteProduct>.Success(favoriteProduct);
        }
        catch
        {
            return Result<FavoriteProduct>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<FavoriteProduct>>> GetFavoritesByUserId(string userId, int pageNumber)
    {
        try
        {
            var page = await _favoriteRepository.GetFavoritesByUserId(
                pageNumber, PageHelper.PageSize, f => f.UserId == userId);
            
            return Result<PagedResponse<FavoriteProduct>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<FavoriteProduct>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<FavoriteProduct>> Add(string userId, string productId)
    {
        try
        {
            var favoriteProduct = new FavoriteProduct
            {
                UserId = userId,
                ProductId = productId
            };
            var result = await _favoriteRepository.Add(favoriteProduct);
            if (result <= 0) return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotCreated);
           
            return Result<FavoriteProduct>.Success(favoriteProduct);
        }
        catch
        {
            return Result<FavoriteProduct>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<FavoriteProduct>> Remove(string userId, string productId)
    {
        try
        {
            var favoriteProduct = await _favoriteRepository.GetByAlternateKey(userId, productId);
            if (favoriteProduct is null) return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotFound);
            
            var result = await _favoriteRepository.Remove(favoriteProduct);
            if (result <= 0) return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotRemoved);
            
            return Result<FavoriteProduct>.Success(favoriteProduct);
        }
        catch
        {
            return Result<FavoriteProduct>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<bool>> Any(string userId)
    {
        try
        {
            var result = await _favoriteRepository.Any(userId);
            return Result<bool>.Success(result);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<bool>> Exists(string userId, string productId)
    {
        try
        {
            var result = await _favoriteRepository.Exists(userId, productId);
            return Result<bool>.Success(result);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
}