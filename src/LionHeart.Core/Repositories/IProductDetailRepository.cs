using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IProductDetailRepository : IRepository<ProductDetail>
{
	Task<int> CountByProductId(string productId);
	Task<List<ProductDetail>> GetByProductId(string productId, int quantity);
}