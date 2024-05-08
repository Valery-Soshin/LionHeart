using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Repositories;

public class CompanyRepository(ApplicationDbContext dbContext) : RepositoryBase<Company>(dbContext), ICompanyRepository
{
    public override Task<Company?> GetById(string id)
    {
        return _dbContext.Companies.AsNoTracking()
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    public Task<Company?> GetByUserId(string userId)
    {
        return _dbContext.Companies.AsNoTracking()
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}