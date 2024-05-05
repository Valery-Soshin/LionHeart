using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.FavoriteProduct;
using LionHeart.BusinessLogic.Helpers;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.FavoriteProduct;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.BusinessLogic.Services;

public class FavoriteProductService : IFavoriteProductService
{
    private readonly IFavoriteProductRepository _favoriteProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly FavoriteProductServiceValidators _validators;
    private readonly UserManager<User> _userManager;

    public FavoriteProductService(IFavoriteProductRepository favoriteRepository,
                                  IProductRepository productRepository,
                                  FavoriteProductServiceValidators validators,
                                  UserManager<User> userManager)
    {
        _favoriteProductRepository = favoriteRepository;
        _productRepository = productRepository;
        _validators = validators;
        _userManager = userManager;
    }

    public async Task<Result<FavoriteProduct>> GetById(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<FavoriteProduct>.Failure(errorMessages);
            }

            var favoriteProduct = await _favoriteProductRepository.GetById(id);
            if (favoriteProduct is null)
            {
                return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotFound);
            }
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
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId, productId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<FavoriteProduct>.Failure(errorMessages);
            }

            var favoriteProduct = await _favoriteProductRepository.GetByAlternateKey(userId, productId);
            if (favoriteProduct is null)
            {
                return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotFound);
            }
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
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<FavoriteProduct>>.Failure(errorMessages);
            }

            var page = await _favoriteProductRepository.GetFavoritesByUserId(
                pageNumber,
                PageHelper.PageSize,
                f => f.UserId == userId);
            
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
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId, productId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<FavoriteProduct>.Failure(errorMessages);
            }

            bool favoriteProductAlreadyExists = await _favoriteProductRepository
                .Exists(f => f.UserId == userId && f.ProductId == productId);
            bool userExists = await _userManager.Users.AnyAsync(u => u.Id == userId);
            bool productExists = await _productRepository.Exists(p => p.Id == productId);
            var validateAddModel = new ValidateAddModel()
            {
                FavoriteProductAlreadyExist = favoriteProductAlreadyExists,
                UserExists = userExists,
                ProductExist = productExists
            };
            var favoriteProductValidatorResult = _validators.FavoriteProductValidator.ValidateAdd(validateAddModel);
            if (favoriteProductValidatorResult.IsFaulted)
            {
                return Result<FavoriteProduct>.Failure(favoriteProductValidatorResult.ErrorMessages);
            }

            var favoriteProduct = new FavoriteProduct
            {
                UserId = userId,
                ProductId = productId
            };
            var favoriteProductRepositoryResult = await _favoriteProductRepository.Add(favoriteProduct);
            if (favoriteProductRepositoryResult <= 0)
            {
                return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotCreated);
            }
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
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId, productId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<FavoriteProduct>.Failure(errorMessages);
            }

            bool favoriteProductExists = await _favoriteProductRepository
                .Exists(f => f.UserId == userId && f.ProductId == productId);
            bool userExists = await _userManager.Users.AnyAsync(u => u.Id == userId);
            bool productExists = await _productRepository.Exists(p => p.Id == productId);
            var validateRemoveModel = new ValidateRemoveModel()
            {
                FavoriteProductExist = favoriteProductExists,
                UserExists = userExists,
                ProductExist = productExists
            };
            var favoriteProductValidatorResult = _validators.FavoriteProductValidator.ValidateRemove(validateRemoveModel);
            if (favoriteProductValidatorResult.IsFaulted)
            {
                return Result<FavoriteProduct>.Failure(favoriteProductValidatorResult.ErrorMessages);
            }

            var favoriteProduct = await _favoriteProductRepository.GetByAlternateKey(userId, productId);
            if (favoriteProduct is null)
            {
                return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotFound);
            }
            var favoriteProductRepositoryResult = await _favoriteProductRepository.Remove(favoriteProduct);
            if (favoriteProductRepositoryResult <= 0)
            {
                return Result<FavoriteProduct>.Failure(ErrorMessage.FavoriteProductNotRemoved);
            }
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
            bool favoriteProductAny = await _favoriteProductRepository.Any(userId);
            return Result<bool>.Success(favoriteProductAny);
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
            bool favoriteProductExists = await _favoriteProductRepository.Exists(userId, productId);
            return Result<bool>.Success(favoriteProductExists);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
}