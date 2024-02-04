using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class BasketRepository(ApplicationDbContext dbContext) : RepositoryBase<Basket>(dbContext), IBasketRepository
{
	public override Task<Basket?> GetById(string id)
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.ProductsInBasket)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
    public override Task<List<Basket>> GetAll()
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.ProductsInBasket)
            .ToListAsync();
    }
    public Task<Basket?> GetByCustomerId(string customerId)
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.ProductsInBasket)
            .FirstOrDefaultAsync(b => b.CustomerId == customerId);
    }
	public Task<bool> HasProduct(string customerId, string productId)
    {
        return _dbContext.Baskets.AsNoTracking()
            .Include(b => b.ProductsInBasket)
            .SelectMany(b => b.ProductsInBasket)
            .Where(p => p.CustomerId == customerId &&
                        p.ProductId == productId)
            .AnyAsync();
    }
}