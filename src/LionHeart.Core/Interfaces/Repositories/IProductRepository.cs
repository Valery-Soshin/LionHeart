using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetProductsByIds(List<string> ids);
    Task<List<Product>> GetProductsByCategoryId(string categoryId);
    Task<List<Product>> GetProductsByUserId(string userId);
    Task<PagedResponse<Product>> GetProductsByCompanyId(string companyId, int pageNumber, int pageSize);
    Task<PagedResponse<Product>> GetProducts(int pageNumber, int pageSize);
    Task<PagedResponse<Product>> Search(string searchedValue, int pageNumber, int pageSize);
}