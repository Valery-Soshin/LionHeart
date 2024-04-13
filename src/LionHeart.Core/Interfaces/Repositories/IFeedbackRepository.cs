using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IFeedbackRepository : IRepository<Feedback>
{
    Task<PagedResponse<Feedback>> GetFeedbacksByUserIdWithPagination(string userId, int pageNumber, int pageSize);
    Task<PagedResponse<Feedback>> GetFeedbacksByProductIdWithPagination(string productId, int pageNumber, int pageSize);
    Task<bool> Exists(string userId, string productId);
}