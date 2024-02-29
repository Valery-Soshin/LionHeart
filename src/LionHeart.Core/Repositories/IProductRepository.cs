using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetProductsByCategoryId(string categoryId);
    Task<List<Product>> GetProductsByUserId(string userId);
}