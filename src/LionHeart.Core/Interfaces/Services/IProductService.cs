using LionHeart.Core.Dtos.Product;
using LionHeart.Core.Models;
using LionHeart.Core.Results;

namespace LionHeart.Core.Interfaces.Services;

public interface IProductService
{
    Task<Result<Product>> GetById(string id);
    Task<Result<PagedResponse<Product>>> GetProductsByCategoryId(string categoryId, int pageNumber);
    Task<Result<PagedResponse<Product>>> GetProductsByUserId(string userId, int pageNumber);
    Task<Result<PagedResponse<Product>>> GetProductsByCompanyId(string companyId, int pageNumber);
    Task<Result<PagedResponse<Product>>> GetProductsByBrandId(string brandId, int pageNumber);
    Task<Result<PagedResponse<Product>>> GetAll(int pageNumber);
    Task<Result<PagedResponse<Product>>> Search(string searchedValue, int pageNumber);
    Task<Result<List<Product>>> FindProducts(List<string> ids);
    Task<Result<Product>> Add(AddProductDto dto);
    Task<Result<Product>> Update(UpdateProductDto dto);
    Task<Result<Product>> Remove(string id);
}