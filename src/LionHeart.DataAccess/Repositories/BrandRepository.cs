using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class BrandRepository(ApplicationDbContext dbContext) : RepositoryBase<Brand>(dbContext), IBrandRepository
{
    public override Task<Brand?> GetById(string id)
    {
        return _dbContext.Brands.AsNoTracking()
            .Include(b => b.Image)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
    public Task<List<Brand>> GetBrands()
    {
        return _dbContext.Brands.AsNoTracking()
            .ToListAsync();
    }
}