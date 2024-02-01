using LionHeart.Core.Enums;
using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class MarkedProductRepository : RepositoryBase<MarkedProduct>, IMarkedProductRepository
{
    public MarkedProductRepository(ApplicationDbContext dbContext)
    : base(dbContext) { }

    public override Task<MarkedProduct?> GetById(string id)
    {
        return _dbContext.MarkedProducts.AsNoTracking()
            .Include(mp => mp.Product)
            .FirstOrDefaultAsync(mp => mp.Id == id);
    }
    public Task<MarkedProduct?> GetByCustomerIdProductId(string customerId, string productId, Mark mark)
    {
        return _dbContext.MarkedProducts.AsNoTracking()
            .Include(mp => mp.Product)
            .FirstOrDefaultAsync(mp => mp.CustomerId == customerId &&
                                       mp.ProductId == productId &&
                                       mp.Mark == mark);
    }
    public override Task<List<MarkedProduct>> GetAll()
    {
        return _dbContext.MarkedProducts.AsNoTracking()
            .Include(mp => mp.Product)
            .ToListAsync();
    }
    public Task<List<MarkedProduct>> GetAllByCustomerId(string customerId, Mark mark)
    {
        return _dbContext.MarkedProducts.AsNoTracking()
            .Include(mp => mp.Product)
            .Where(mp => mp.CustomerId == customerId && mp.Mark == mark)
            .ToListAsync();
    }
}