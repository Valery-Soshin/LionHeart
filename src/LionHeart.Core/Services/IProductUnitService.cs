using LionHeart.Core.Enums;
using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IProductUnitService
{
	Task<ProductUnit?> GetById(string id);
	Task<List<ProductUnit>> GetByProductId(string productId, int quantity);
	Task<List<ProductUnit>> GetAll();
	Task<int> Add(ProductUnit product);
	Task<int> Update(ProductUnit product);
	Task<int> Remove(ProductUnit product);
	Task<int> CountByProductId(string productId);
}