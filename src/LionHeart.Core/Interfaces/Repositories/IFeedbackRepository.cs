using LionHeart.Core.Models;
using System.Linq.Expressions;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IFeedbackRepository : IRepository<Feedback>
{
    Task<PagedResponse<Feedback>> GetFeedbacksByFilter(int pageNumber, int pageSize, Expression<Func<Feedback, bool>> filter);
    Task<bool> Exists(string userId, string productId);
}