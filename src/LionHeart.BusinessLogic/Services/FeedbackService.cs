using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _repository;
    private readonly IOrderService _orderService;

    public FeedbackService(IFeedbackRepository repository,
                           IOrderService orderService)
    {
        _repository = repository;
        _orderService = orderService;
    }

    public Task<Feedback?> GetById(string id)
    {
        return _repository.GetById(id);
    }
    public Task<List<Feedback>> GetAll()
    {
        return _repository.GetAll();
    }
    public Task Add(Feedback feedback)
    {
        return _repository.Add(feedback);
    }
    public Task Update(Feedback feedback)
    {
        return _repository.Update(feedback);
    }
    public Task Remove(Feedback feedback)
    {
        return _repository.Remove(feedback);
    }
    public async Task<bool> HasFeedbackPending(string userId, string productId)
    {
        return !await _repository.Exists(userId, productId) &&
            await _orderService.Exists(userId, productId);
    }
}