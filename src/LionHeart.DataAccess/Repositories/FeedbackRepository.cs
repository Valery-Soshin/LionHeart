using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class FeedbackRepository(ApplicationDbContext dbContext) : RepositoryBase<Feedback>(dbContext), IFeedbackRepository
{
    public override Task<Feedback?> GetById(string id)
    {
        return _dbContext.Feedbacks.AsNoTracking()
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    public override Task<List<Feedback>> GetAll()
    {
        return _dbContext.Feedbacks.AsNoTracking()
            .Include(f => f.User)
            .ToListAsync();
    }
    public Task<bool> Exists(string userId, string productId)
    {
        return _dbContext.Feedbacks.AsNoTracking()
            .AnyAsync(o => o.UserId == userId &&
                           o.ProductId == productId);
    }
}