using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class FavoriteProductRepository(ApplicationDbContext dbContext) : RepositoryBase<FavoriteProduct>(dbContext), IFavoriteProductRepository
{
    public override Task<FavoriteProduct?> GetById(string id)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
                .ThenInclude(p => p.Image)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    public Task<FavoriteProduct?> GetByUserIdProductId(string userId, string productId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
                .ThenInclude(p => p.Image)
            .FirstOrDefaultAsync(f => f.UserId == userId &&
                                      f.ProductId == productId);
    }
    public override Task<List<FavoriteProduct>> GetAll()
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
                .ThenInclude(p => p.Image)
            .ToListAsync();
    }
    public Task<List<FavoriteProduct>> GetAllByUserId(string userId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
                .ThenInclude(p => p.Image)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }
    public Task<bool> Any(string userId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .AnyAsync(f => f.UserId == userId);
    }
    public Task<bool> Exists(string userId, string productId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .AnyAsync(f => f.UserId == userId &&
                           f.ProductId == productId);
    }
}