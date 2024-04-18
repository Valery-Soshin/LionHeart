using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class CategoryRepository(ApplicationDbContext dbContext) : RepositoryBase<Category>(dbContext), ICategoryRepository
{
    public override Task<Category?> GetById(string id)
    {
        return _dbContext.Categories.AsNoTracking()
            .Include(c => c.SubCategories)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    public Task<List<Category>> GetParentCategories()
    {
        return _dbContext.Categories
            .Where(c => c.ParentCategoryId == null)
            .ToListAsync();
    }
}