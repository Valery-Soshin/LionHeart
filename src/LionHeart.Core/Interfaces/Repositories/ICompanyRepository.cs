using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface ICompanyRepository : IRepository<Company>
{
    Task<Company?> GetByUserId(string userId);
}