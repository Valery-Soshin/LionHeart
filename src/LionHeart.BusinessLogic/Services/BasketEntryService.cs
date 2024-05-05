using LionHeart.BusinessLogic.Resources;
using LionHeart.BusinessLogic.FluentValidations.Validators.BasketEntry;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.BasketEntry;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.BasketEntry;

namespace LionHeart.BusinessLogic.Services;

public class BasketEntryService : IBasketEntryService
{
    private readonly IBasketEntryRepository _basketEntryRepository;
    private readonly IProductRepository _productRepository;
    private readonly BasketEntryServiceValidators _validators;

    public BasketEntryService(IBasketEntryRepository basketEntryRepository,
                              IProductRepository productRepository,
                              BasketEntryServiceValidators validators)
    {
        _basketEntryRepository = basketEntryRepository;
        _productRepository = productRepository;
        _validators = validators;
    }

    public async Task<Result<BasketEntry>> GetById(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<BasketEntry>.Failure(errorMessages);
            }

            var entry = await _basketEntryRepository.GetById(id);
            if (entry is null)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotFound);
            }

            return Result<BasketEntry>.Success(entry);
        }
        catch
        {
            return Result<BasketEntry>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<BasketEntry>> GetByAlternateKey(string userId, string productId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId, productId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<BasketEntry>.Failure(errorMessages);
            }

            var entry = await _basketEntryRepository.GetByAlternateKey(userId, productId);
            if (entry is null)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotFound);
            }
            return Result<BasketEntry>.Success(entry);
        }
        catch
        {
            return Result<BasketEntry>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<BasketEntry>>> GetEntriesByUserId(string userId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<List<BasketEntry>>.Failure(errorMessages);
            }

            var entries = await _basketEntryRepository.GetEntriesByUserId(userId);
            return Result<List<BasketEntry>>.Success(entries);
        }
        catch
        {
            return Result<List<BasketEntry>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<BasketEntry>> Add(AddBasketEntryDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.AddBasketEntryDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<BasketEntry>.Failure(errorMessages);
            }

            bool basketEntryAlreadyExists = await _basketEntryRepository.Exists(dto.UserId, dto.ProductId);
            bool productExists = await _productRepository.Exists(p => p.Id == dto.ProductId);

            var validateAddModel = new ValidateAddModel()
            {
                BasketEntryAlreadyExists = basketEntryAlreadyExists,
                ProductExists = productExists
            };
            var basketEntryValidatorResult = _validators.BasketEntryValidator.ValidateAdd(validateAddModel);
            if (basketEntryValidatorResult.IsFaulted)
            {
                return Result<BasketEntry>.Failure(basketEntryValidatorResult.ErrorMessages);
            }

            var entry = new BasketEntry()
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                CreatedAt = dto.CreatedAt
            };
            var basketEntryRepositoryResult = await _basketEntryRepository.Add(entry);
            if (basketEntryRepositoryResult <= 0)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotCreated);
            }

            return Result<BasketEntry>.Success(entry);
        }
        catch
        {
            return Result<BasketEntry>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<BasketEntry>> Update(UpdateBasketEntryDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.UpdateBasketEntryDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<BasketEntry>.Failure(errorMessages);
            }

            bool basketEntryExists = await _basketEntryRepository.Exists(e => e.Id == dto.Id);
            var validateUpdateModel = new ValidateUpdateModel()
            {
                BasketEntryExists = basketEntryExists,
            };
            var basketEntryValidatorResult = _validators.BasketEntryValidator.ValidateUpdate(validateUpdateModel);
            if (basketEntryValidatorResult.IsFaulted)
            {
                return Result<BasketEntry>.Failure(basketEntryValidatorResult.ErrorMessages);
            }

            var entry = await _basketEntryRepository.GetById(dto.Id);
            if (entry is null)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotFound);
            }
            entry.Quantity = dto.Quantity;
            var basketEntryRepositoryResult = await _basketEntryRepository.Update(entry);
            if (basketEntryRepositoryResult <= 0)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotUpdated);
            }
            return Result<BasketEntry>.Success(entry);
        }
        catch
        {
            return Result<BasketEntry>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<BasketEntry>> Remove(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<BasketEntry>.Failure(errorMessages);
            }

            bool basketEntryExists = await _basketEntryRepository.Exists(e => e.Id == id);
            var validateRemoveModel = new ValidateRemoveModel()
            {
                BasketEntryExists = basketEntryExists,
            };
            var basketEntryValidatorResult = _validators.BasketEntryValidator.ValidateRemove(validateRemoveModel);
            if (basketEntryValidatorResult.IsFaulted)
            {
                return Result<BasketEntry>.Failure(basketEntryValidatorResult.ErrorMessages);
            }

            var entry = await _basketEntryRepository.GetById(id);
            if (entry is null)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotFound);
            }
            var basketEntryRepositoryResult = await _basketEntryRepository.Remove(entry);
            if (basketEntryRepositoryResult <= 0)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotDeleted);
            }
            return Result<BasketEntry>.Success(entry);
        }
        catch
        {
            return Result<BasketEntry>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<BasketEntry>>> RemoveRange(List<string> ids)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(ids));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<List<BasketEntry>>.Failure(errorMessages);
            }

            var entries = await _basketEntryRepository.Find(ids);
            if (entries.Count == 0 || entries.Count != ids.Count)
            {
                return Result<List<BasketEntry>>.Failure(ErrorMessage.BasketEntriesNotFound);
            }
            var basketEntryRepositoryResult = await _basketEntryRepository.RemoveRange(entries);
            if (basketEntryRepositoryResult <= 0)
            {
                return Result<List<BasketEntry>>.Failure(ErrorMessage.BasketEntriesNotRemoved);
            }
            return Result<List<BasketEntry>>.Success(entries);
        }
        catch
        {
            return Result<List<BasketEntry>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<bool>> Exists(string userId, string productId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId, productId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<bool>.Failure(errorMessages);
            }

            bool basketEntryExists = await _basketEntryRepository.Exists(userId, productId);
            return Result<bool>.Success(basketEntryExists);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
}