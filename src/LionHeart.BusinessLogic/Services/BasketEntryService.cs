using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Response;
using Microsoft.Extensions.Logging;

namespace LionHeart.BusinessLogic.Services;

public class BasketEntryService : IBasketEntryService
{
    private readonly IBasketEntryRepository _basketEntryRepository;
    private readonly ILogger<BasketEntryService> _logger;

    public BasketEntryService(IBasketEntryRepository basketEntryRepository,
                              ILogger<BasketEntryService> logger)
    {
        _basketEntryRepository = basketEntryRepository;
        _logger = logger;
    }

    public async Task<IBaseResponse<BasketEntry>> GetById(string id)
    {
        try
        {
            var basketEntry = await _basketEntryRepository.GetById(id);
            if (basketEntry is null)
            {
                return new BaseResponse<BasketEntry>()
                {
                    IsCompleted = false,
                    Description = $"The basket entry with ID '{id}' has not been received"
                };
            }

            _logger.LogInformation("The basket entry  with ID '{id}' has been received", id);
            return new BaseResponse<BasketEntry>()
            {
                IsCompleted = true,
                Description = $"The basket entry  with ID '{id}' has been received",
                Data = basketEntry
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The basket entry  with ID '{id}' has not been: {ex}", id, ex.Message);
            return new BaseResponse<BasketEntry>()
            {
                IsCompleted = false,
                Description = $"The basket entry  with ID '{id}' has been received: {ex.Message}"
            };
        }
    }
    public Task<BasketEntry?> GetByUserIdProductId(string userId, string productId)
    {
        return _basketEntryRepository.GetByUserIdProductId(userId, productId);
    }
    public Task<List<BasketEntry>> GetEntriesByUserId(string userId)
    {
        return _basketEntryRepository.GetEntriesByUserId(userId);
    }
    public Task<int> Add(BasketEntry entry)
    {
        return _basketEntryRepository.Add(entry);
    }
    public Task<int> Update(BasketEntry entry)
    {
        return _basketEntryRepository.Update(entry);
    }
    public Task<int> Remove(BasketEntry entry)
    {
        return _basketEntryRepository.Remove(entry);
    }
    public Task<int> Remove(string id)
    {
        return _basketEntryRepository.Remove(id);
    }
}