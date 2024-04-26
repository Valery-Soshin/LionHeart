using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.BasketEntry;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class BasketEntryService : IBasketEntryService
{
    private readonly IBasketEntryRepository _basketEntryRepository;

    public BasketEntryService(IBasketEntryRepository basketEntryRepository)
    {
        _basketEntryRepository = basketEntryRepository;
    }

    public async Task<Result<BasketEntry>> GetById(string id)
    {
        try
        {
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
            var entry = new BasketEntry
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                CreatedAt = dto.CreatedAt
            };
            bool entryAlreadyExists = await _basketEntryRepository.Exists(dto.UserId, dto.ProductId);
            if (entryAlreadyExists) return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryAlreadyExists);
            var result = await _basketEntryRepository.Add(entry);
            if (result <= 0) return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotCreated);
            
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
            var entry = await _basketEntryRepository.GetById(dto.Id);
            if (entry is null)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotFound);
            }
            entry.Quantity = dto.Quantity;
            var result = await _basketEntryRepository.Update(entry);
            if (result <= 0)
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
            var entry = await _basketEntryRepository.GetById(id);
            if (entry is null)
            {
                return Result<BasketEntry>.Failure(ErrorMessage.BasketEntryNotFound);
            }
            var result = await _basketEntryRepository.Remove(entry);
            if (result <= 0)
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
            var entries = await _basketEntryRepository.Find(ids);
            if (entries.Count == 0)
            {
                return Result<List<BasketEntry>>.Failure(ErrorMessage.BasketEntriesNotFound);
            }
            var result = await _basketEntryRepository.RemoveRange(entries);
            if (result <= 0)
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
            var result = await _basketEntryRepository.Exists(userId, productId);
            return Result<bool>.Success(result);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
}