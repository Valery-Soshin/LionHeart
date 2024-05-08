using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Repositories;

public class FavoriteProductRepository(ApplicationDbContext dbContext) : RepositoryBase<FavoriteProduct>(dbContext), IFavoriteProductRepository
{
    public override Task<FavoriteProduct?> GetById(string id)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product)
                .ThenInclude(p => p.Image)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    public Task<FavoriteProduct?> GetByAlternateKey(string userId, string productId)
    {
        return _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product.Image)
            .SingleOrDefaultAsync(f => f.UserId == userId &&
                                      f.ProductId == productId);
    }
    public Task<PagedResponse<FavoriteProduct>> GetFavoritesByUserId(int pageNumber, int pageSize, Expression<Func<FavoriteProduct, bool>> filter)
    {
        return ExecutePagination(pageNumber, pageSize, filter);
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

    private Task<PagedResponse<FavoriteProduct>> ExecutePagination(int pageNumber, int pageSize, Expression<Func<FavoriteProduct, bool>> filter)
    {
        var totalRecordsQuery = _dbContext.FavoriteProducts.AsNoTracking();

        var favoriteProductsQuery = _dbContext.FavoriteProducts.AsNoTracking()
            .Include(f => f.Product.Image);

        return BuildPagination(totalRecordsQuery, favoriteProductsQuery, pageNumber, pageSize, filter);
    }
}