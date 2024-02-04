using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IProductUnitRepository : IRepository<ProductUnit>
{
	Task<int> CountByProductId(string productId);
	Task<List<ProductUnit>> GetByProductId(string productId, int quantity);
}