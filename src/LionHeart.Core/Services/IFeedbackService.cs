using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IFeedbackService
{
    Task<Feedback?> GetById(string id);
    Task<List<Feedback>> GetAll();
    Task Add(Feedback feedback);
    Task Update(Feedback feedback);
    Task Remove(Feedback feedback);
    Task<bool> Exists(string userId, string productId);
}