using LionHeart.Core.Models;
using System.Linq.Expressions;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByName(string name);
    Task<List<Product>> FindProducts(List<string> ids);
    Task<PagedResponse<Product>> GetProductsByFilter(int pageNumber, int pageSize, Expression<Func<Product, bool>> filter);
    Task<PagedResponse<Product>> GetProducts(int pageNumber, int pageSize);
    Task<PagedResponse<Product>> Search(string searchedValue, int pageNumber, int pageSize);
}