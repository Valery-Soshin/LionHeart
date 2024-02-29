using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IProductService
{
    Task<Product?> GetById(string id);
    Task<List<Product>> GetAll();
    Task<List<Product>> GetProductsByCategoryId(string categoryId);
    Task<List<Product>> GetProductsByUserId(string userId);
    Task<int> Add(Product product);
    Task<int> Update(Product product);
    Task<int> Remove(Product product);
}