using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

	public ProductService(IProductRepository productRepository)
    {
        _repository = productRepository;
    }

    public Task<Product?> GetById(string id)
    {
        return _repository.GetById(id);
    }
    public Task<List<Product>> GetAll()
    {
        return _repository.GetAll();
    }
    public Task<List<Product>> GetProductsByCategoryId(string categoryId)
    {
        return _repository.GetProductsByCategoryId(categoryId);
    }
    public Task<List<Product>> GetProductsByUserId(string userId)
    {
        return _repository.GetProductsByUserId(userId);
    }
    public Task<List<Product>> Search(string productName)
    {
        return _repository.Search(productName);
    }
    public Task<int> Add(Product product)
    {
        return _repository.Add(product);
    }
    public Task<int> Update(Product product)
    {
        return _repository.Update(product);
    }
    public Task<int> Remove(Product product)
    {
        return _repository.Remove(product);
    }
}