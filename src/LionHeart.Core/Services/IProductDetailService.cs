using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IProductDetailService
{
	Task<ProductDetail?> GetById(string id);
	Task<List<ProductDetail>> GetByProductId(string productId, int quantity);
	Task<List<ProductDetail>> GetAll();
	Task<int> Add(ProductDetail product);
	Task<int> Update(ProductDetail product);
	Task<int> Remove(ProductDetail product);
	Task<int> CountByProductId(string productId);
}