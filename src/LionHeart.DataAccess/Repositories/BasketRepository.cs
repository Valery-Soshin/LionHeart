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
                .ThenInclude(p => p.Product)
            .ToListAsync();
    }
    public Task<Basket?> GetByCustomerId(string userId)
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.Products)
                .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(b => b.UserId == userId);
    }
	public Task<bool> HasProduct(string userId, string productId)
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.Products)
            .SelectMany(b => b.Products)
            .Where(p => p.UserId == userId &&
                        p.ProductId == productId)
            .AnyAsync();
    }
}