using LionHeart.Core.Enums;
using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IProductUnitRepository : IRepository<ProductUnit>
{
    Task<List<ProductUnit>> GetByProductId(string productId, int quantity);
    Task<int> CountByProductId(string productId);
}