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
                return new Result<BasketEntry>()
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntryNotFound
                };
            }
            return new Result<BasketEntry>()
            {
                IsCompleted = true,
                ErrorMessage = ErrorMessage.BasketEntryFound,
                Data = entry
            };
        }
        catch
        {
            return new Result<BasketEntry>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<BasketEntry>> GetByUserIdProductId(string userId, string productId)
    {
        try
        {
            var entry = await _basketEntryRepository.GetByUserIdProductId(userId, productId);
            if (entry is null)
            {
                return new Result<BasketEntry>()
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntryNotFound
                };
            }
            return new Result<BasketEntry>()
            {
                IsCompleted = true,
                ErrorMessage = ErrorMessage.BasketEntryFound,
                Data = entry
            };
        }
        catch
        {
            return new Result<BasketEntry>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<BasketEntry>>> GetEntriesByUserId(string userId)
    {
        try
        {
            var entries = await _basketEntryRepository.GetEntriesByUserId(userId);
            if (entries is null)
            {
                return new Result<List<BasketEntry>>()
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntriesNotFound
                };
            }
            return new Result<List<BasketEntry>>()
            {
                IsCompleted = true,
                ErrorMessage = ErrorMessage.BasketEntriesFound,
                Data = entries
            };
        }
        catch
        {
            return new Result<List<BasketEntry>>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
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
                Quantity = dto.Quantity
            };
            var result = await _basketEntryRepository.Add(entry);
            if (result <= 0)
            {
                return new Result<BasketEntry>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntryNotCreated,
                };
            }

            return new Result<BasketEntry>
            {
                IsCompleted = true,
                ErrorMessage = ErrorMessage.BasketEntryCreated,
                Data = entry
            };
        }
        catch
        {
            return new Result<BasketEntry>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError,
            };
        }

    }
    public async Task<Result<BasketEntry>> Update(UpdateBasketEntryDto dto)
    {
        try
        {
            var entry = await _basketEntryRepository.GetById(dto.Id);
            if (entry is null)
            {
                return new Result<BasketEntry>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntryNotFound,
                };
            }

            entry.Quantity = dto.Quantity;

            var result = await _basketEntryRepository.Update(entry);
            if (result <= 0)
            {
                return new Result<BasketEntry>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntryNotUpdated,
                };
            }
            return new Result<BasketEntry>
            {
                IsCompleted = true,
                ErrorMessage = ErrorMessage.BasketEntryUpdated,
                Data = entry
            };
        }
        catch
        {
            return new Result<BasketEntry>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError,
            };
        }
    }
    public async Task<Result<BasketEntry>> Remove(string id)
    {
        try
        {
            var entry = await _basketEntryRepository.GetById(id);
            if (entry is null)
            {
                return new Result<BasketEntry>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntryNotFound,
                };
            }
            var result = await _basketEntryRepository.Remove(entry);
            if (result <= 0)
            {
                return new Result<BasketEntry>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntryNotDeleted,
                };
            }
            return new Result<BasketEntry>
            {
                IsCompleted = true,
                ErrorMessage = ErrorMessage.BasketEntryDeleted,
                Data = entry
            };
        }
        catch
        {
            return new Result<BasketEntry>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError,
            };
        }
    }
    public async Task<Result<List<BasketEntry>>> RemoveRange(List<string> ids)
    {
        try
        {
            var entries = await _basketEntryRepository.GetAll(ids);
            var result = await _basketEntryRepository.RemoveRange(entries);
            if (result <= 0)
            {
                return new Result<List<BasketEntry>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BasketEntriesNotRemoved
                };
            }
            return new Result<List<BasketEntry>>
            {
                IsCompleted = true,
                Data = entries
            };
        }
        catch
        {
            return new Result<List<BasketEntry>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError,
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
                Data = await _basketEntryRepository.Exists(userId, productId)
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