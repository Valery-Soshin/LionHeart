using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class BasketEntryRepository : RepositoryBase<BasketEntry>, IBasketEntryRepository
{
    public BasketEntryRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public override Task<BasketEntry?> GetById(string id)
    {
        return _dbContext.BasketEntries.AsNoTracking()
            .Include(e => e.Product)
                .ThenInclude(p => p.Image)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<BasketEntry?> GetByUserIdProductId(string userId, string productId)
    {
        return _dbContext.BasketEntries.AsNoTracking()
            .Include(e => e.Product)
                .ThenInclude(p => p.Image)
            .FirstOrDefaultAsync(e => e.UserId == userId &&
                                      e.ProductId == productId);
    }
    public Task<List<BasketEntry>> GetEntriesByUserId(string userId)
    {
        return _dbContext.BasketEntries.AsNoTracking()
            .Include(e => e.Product)
                .ThenInclude(p => p.Image)
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }
    public override Task<List<BasketEntry>> GetAll()
    {
        return _dbContext.BasketEntries.AsNoTracking()
            .Include(e => e.Product)
                .ThenInclude(p => p.Image)
            .ToListAsync();
    }
}