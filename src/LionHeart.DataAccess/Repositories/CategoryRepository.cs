using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class CategoryRepository(ApplicationDbContext dbContext) : RepositoryBase<Category>(dbContext), ICategoryRepository
{
    public override Task<Category?> GetById(string id)
    {
        return _dbContext.Categories.AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    public override Task<List<Category>> GetAll()
    {
        return _dbContext.Categories.AsNoTracking()
            .ToListAsync();
    }
}