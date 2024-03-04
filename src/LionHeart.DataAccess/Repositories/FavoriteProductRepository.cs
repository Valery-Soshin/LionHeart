using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionHeart.DataAccess.Repositories;

public class FavoriteProductRepository(ApplicationDbContext dbContext) : RepositoryBase<FavoriteProduct>(dbContext), IFavoriteProductRepository
{
    public override Task<FavoriteProduct?> GetById(string id)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    public Task<FavoriteProduct?> GetByUserIdProductId(string userId, string productId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
            .FirstOrDefaultAsync(f => f.UserId == userId &&
                                      f.ProductId == productId);
    }
    public override Task<List<FavoriteProduct>> GetAll()
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
            .ToListAsync();
    }
    public Task<List<FavoriteProduct>> GetAllByUserId(string userId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }
    public Task<bool> Any(string userId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .AnyAsync(f => f.UserId == userId);
    }
    public Task<bool> Any(string userId, string productId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .AnyAsync(f => f.UserId == userId &&
                           f.ProductId == productId);
    }
}