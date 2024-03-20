using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
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