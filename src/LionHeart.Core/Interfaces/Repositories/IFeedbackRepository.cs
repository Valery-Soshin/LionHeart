using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IFeedbackRepository : IRepository<Feedback>
{
    Task<bool> Exists(string userId, string productId);
}