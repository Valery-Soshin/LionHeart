using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionHeart.DataAccess.Repositories;

public class FeedbackRepository(ApplicationDbContext dbContext) : RepositoryBase<Feedback>(dbContext), IFeedbackRepository
{
    public override Task<Feedback?> GetById(string id)
    {
        return _dbContext.Feedbacks.AsNoTracking()
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    public Task<PagedResponse<Feedback>> GetFeedbacksByFilter(int pageNumber, int pageSize, Expression<Func<Feedback, bool>> filter)
    {
        return ExecutePagination(pageNumber, pageSize, filter);
    }
    private Task<PagedResponse<Feedback>> ExecutePagination(int pageNumber, int pageSize, Expression<Func<Feedback, bool>>? filter = null) 
    {
        var totalRecordsQuery = _dbContext.Feedbacks.AsNoTracking();

        var feedbacksQuery = _dbContext.Feedbacks.AsNoTracking()
            .Include(f => f.User)
            .Include(f => f.Product.Image);

        return BuildPagination(totalRecordsQuery, feedbacksQuery, pageNumber, pageSize, filter);
    }
    public Task<bool> Exists(string userId, string productId)
    {
        return _dbContext.Feedbacks.AsNoTracking()
            .AnyAsync(o => o.UserId == userId &&
                           o.ProductId == productId);
    }
}