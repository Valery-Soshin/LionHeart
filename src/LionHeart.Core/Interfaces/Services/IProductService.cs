using LionHeart.Core.Dtos.Product;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IProductService
{
    Task<Result<Product>> GetById(string id);
    Task<Result<List<Product>>> GetAll();
    Task<Result<List<Product>>> GetProductsByCategoryId(string categoryId);
    Task<Result<List<Product>>> GetProductsByUserId(string userId);
    Task<Result<PagedResponse>> GetProductsWithPagination(int pageNumber);
    Task<Result<List<Product>>> Search(string productName);
    Task<Result<Product>> Add(AddProductDto dto);
    Task<Result<Product>> Update(UpdateProductDto dto);
    Task<Result<Product>> Remove(RemoveProductDto dto);
}