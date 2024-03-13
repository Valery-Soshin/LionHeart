using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _repository;

    public FeedbackService(IFeedbackRepository repository)
    {
        _repository = repository;
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
    public Task<bool> Exists(string userId, string productId)
    {
        return _repository.Exists(userId, productId);
    }
}