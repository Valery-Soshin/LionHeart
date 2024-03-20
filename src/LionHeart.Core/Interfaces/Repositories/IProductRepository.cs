using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetProductsByCategoryId(string categoryId);
    Task<List<Product>> GetProductsByUserId(string userId);
    Task<List<Product>> Search(string productName);
}