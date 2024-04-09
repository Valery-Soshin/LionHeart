using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IProductUnitRepository : IRepository<ProductUnit>
{
    Task<List<ProductUnit>> GetByProductId(string productId, int quantity);
    Task<List<ProductUnit>> GetProductsByIds(List<string> ids);
    Task<int> Count(string productId);
}