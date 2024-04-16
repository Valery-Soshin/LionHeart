using LionHeart.Core.Dtos.Product;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IProductService
{
    Task<Result<Product>> GetById(string id);
    Task<Result<List<Product>>> GetProductsByIds(List<string> ids);
    Task<Result<List<Product>>> GetProductsByCategoryId(string categoryId);
    Task<Result<List<Product>>> GetProductsByUserId(string userId);
    Task<Result<PagedResponse<Product>>> GetProductsWithPagination(int pageNumber);
    Task<Result<PagedResponse<Product>>> Search(string productName, int pageNumbere);
    Task<Result<Product>> Add(AddProductDto dto);
    Task<Result<Product>> Update(UpdateProductDto dto);
    Task<Result<Product>> Remove(string id);
}