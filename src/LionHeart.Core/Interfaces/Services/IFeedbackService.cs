using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Services;

public interface IFeedbackService
{
    Task<Feedback?> GetById(string id);
    Task<List<Feedback>> GetAll();
    Task Add(Feedback feedback);
    Task Update(Feedback feedback);
    Task Remove(Feedback feedback);
    Task<bool> HasFeedbackPending(string userId, string productId);
}