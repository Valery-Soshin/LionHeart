using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.FavoriteProduct;
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
            if (favoriteProduct is null)
            {
                return new Result<FavoriteProduct>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FavoriteProductNotFound
                };
            }
            return new Result<FavoriteProduct>
            {
                IsCompleted = true,
                Data = favoriteProduct
            };
        }
        catch
        {
            return new Result<FavoriteProduct>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<FavoriteProduct>> GetByUserIdProductId(string userId, string productId)
    {
        try
        {
            var favoriteProduct = await _favoriteRepository.GetByUserIdProductId(userId, productId);
            if (favoriteProduct is null)
            {
                return new Result<FavoriteProduct>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FavoriteProductNotFound
                };
            }
            return new Result<FavoriteProduct>
            {
                IsCompleted = true,
                Data = favoriteProduct
            };
        }
        catch
        {
            return new Result<FavoriteProduct>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<FavoriteProduct>>> GetFavoritesByUserIdWithoutQueryFilter(string userId)
    {
        try
        {
            var favoriteProducts = await _favoriteRepository.GetFavoritesByUserIdWithoutQueryFilter(userId);
            if (favoriteProducts is null)
            {
                return new Result<List<FavoriteProduct>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FavoriteProductsNotFound
                };
            }
            return new Result<List<FavoriteProduct>>
            {
                IsCompleted = true,
                Data = favoriteProducts
            };
        }
        catch
        {
            return new Result<List<FavoriteProduct>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
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
            if (result <= 0)
            {
                return new Result<FavoriteProduct>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FavoriteProductNotCreated
                };
            }
            return new Result<FavoriteProduct>
            {
                IsCompleted = true,
                Data = favoriteProduct
            };
        }
        catch
        {
            return new Result<FavoriteProduct>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<FavoriteProduct>> Remove(string userId, string productId)
    {
        try
        {
            var favoriteProduct = await _favoriteRepository
                .GetByUserIdProductId(userId, productId);

            if (favoriteProduct is null)
            {
                return new Result<FavoriteProduct>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FavoriteProductNotFound
                };
            }
            var result = await _favoriteRepository.Remove(favoriteProduct);
            if (result <= 0)
            {
                return new Result<FavoriteProduct>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FavoriteProductNotCreated
                };
            }
            return new Result<FavoriteProduct>
            {
                IsCompleted = true,
                Data = favoriteProduct
            };
        }
        catch
        {
            return new Result<FavoriteProduct>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<bool>> Any(string userId)
    {
        try
        {
            return new Result<bool>
            {
                IsCompleted = true,
                Data = await _favoriteRepository.Any(userId)
            };
        }
        catch
        {
            return new Result<bool>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<bool>> Exists(string userId, string productId)
    {
        try
        {
            return new Result<bool>
            {
                IsCompleted = true,
                Data = await _favoriteRepository.Exists(userId, productId)
            };
        }
        catch
        {
            return new Result<bool>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}