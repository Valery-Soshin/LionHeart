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
    public async Task<PagedResponse<Feedback>> GetFeedbacksByUserIdWithPagination(string userId, int pageNumber, int pageSize)
    {
        var totalRecords = await _dbContext.Feedbacks.AsNoTracking()
            .Where(f => f.UserId == userId)
            .CountAsync();

        var feedbacks = await _dbContext.Feedbacks.AsNoTracking()
            .Include(f => f.Product)
                .ThenInclude(p => p.Image)
            .Where(f => f.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<Feedback>(feedbacks, totalRecords, pageNumber, pageSize);
    }
    public async Task<PagedResponse<Feedback>> GetFeedbacksByProductIdWithPagination(string productId, int pageNumber, int pageSize)
    {
        var totalRecords = await _dbContext.Feedbacks.AsNoTracking()
            .Where(f => f.ProductId == productId)
            .CountAsync();

        var feedbacks = await _dbContext.Feedbacks.AsNoTracking()
            .Include(f => f.User)
            .Where(f => f.ProductId == productId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<Feedback>(feedbacks, totalRecords, pageNumber, pageSize);
    }
    public Task<bool> Exists(string userId, string productId)
    {
        return _dbContext.Feedbacks.AsNoTracking()
            .AnyAsync(o => o.UserId == userId &&
                           o.ProductId == productId);
    }
}