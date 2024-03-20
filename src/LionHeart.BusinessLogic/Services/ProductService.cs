using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;

namespace LionHeart.BusinessLogic.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IImageService _imageService;

    public ProductService(IProductRepository productRepository,
                          IImageService imageService)
    {
        _repository = productRepository;
        _imageService = imageService;
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
    public async Task Add(Product product)
    {
        if (product.Image is null || product.Image.File is null) return;

        await _imageService.Add(product.Image.File);
        await _repository.Add(product);
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