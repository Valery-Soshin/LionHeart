using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IFeedbackRepository : IRepository<Feedback>
{
    Task<PagedResponse<Feedback>> GetFeedbacksWithPagination(string productId, int pageNumber, int pageSize);
    Task<bool> Exists(string userId, string productId);
}