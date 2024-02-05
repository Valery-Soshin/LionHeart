using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class BasketRepository(ApplicationDbContext dbContext) : RepositoryBase<Basket>(dbContext), IBasketRepository
{
	public override Task<Basket?> GetById(string id)
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.Products)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
    public override Task<List<Basket>> GetAll()
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.Products)
                .ThenInclude(p => p.Information)
            .ToListAsync();
    }
    public Task<Basket?> GetByCustomerId(string customerId)
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.Products)
                .ThenInclude(p => p.Information)
            .FirstOrDefaultAsync(b => b.UserId == customerId);
    }
	public Task<bool> HasProduct(string customerId, string productId)
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.Products)
            .SelectMany(b => b.Products)
            .Where(p => p.UserId == customerId &&
                        p.ProductId == productId)
            .AnyAsync();
    }
}