using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IProductService
{
    Task<Product?> GetById(string id);
    Task<List<Product>> GetAll();
    Task<List<Product>> GetProductsByCategoryId(string categoryId);
    Task<List<Product>> GetProductsByUserId(string userId);
    Task<List<Product>> Search(string productName);
    Task Add(Product product);
    Task<int> Update(Product product);
    Task<int> Remove(Product product);
}