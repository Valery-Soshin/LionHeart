using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<Product?> GetById(string id)
    {
        return _productRepository.GetById(id);
    }
    public Task<List<Product>> GetAll()
    {
        return _productRepository.GetAll();
    }
    public Task<List<Product>> GetProductsByCategoryId(string categoryId)
    {
        return _productRepository.GetProductsByCategoryId(categoryId);
    }
    public Task<int> Add(Product product)
    {
        return _productRepository.Add(product);
    }
    public Task<int> Update(Product product)
    {
        return _productRepository.Update(product);
    }
    public Task<int> Remove(Product product)
    {
        return _productRepository.Remove(product);
    }
}