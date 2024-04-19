using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    Task<List<Brand>> GetBrands();
}