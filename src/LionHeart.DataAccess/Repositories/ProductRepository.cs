using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : RepositoryBase<Product>(dbContext), IProductRepository
{
    public override Task<Product?> GetById(string id)
    {
        return _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Feedbacks)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public override Task<List<Product>> GetAll()
    {
        return _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Feedbacks)
            .ToListAsync();
    }
    public Task<List<Product>> GetProductsByCategoryId(string categoryId)
    {
        return _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Feedbacks)
            .Where(p => p.Category.Id == categoryId)
            .ToListAsync();
    }
}