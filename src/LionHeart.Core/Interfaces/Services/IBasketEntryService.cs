using LionHeart.Core.Dtos.BasketEntry;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IBasketEntryService
{
    Task<Result<BasketEntry>> GetById(string id);
    Task<Result<BasketEntry>> GetByAlternateKey(string userId, string productId);
    Task<Result<List<BasketEntry>>> GetEntriesByUserId(string userId);
    Task<Result<BasketEntry>> Add(AddBasketEntryDto dto);
    Task<Result<BasketEntry>> Update(UpdateBasketEntryDto dto);
    Task<Result<BasketEntry>> Remove(string id);
    Task<Result<List<BasketEntry>>> RemoveRange(List<string> ids);
    Task<Result<bool>> Exists(string userId, string productId);
}