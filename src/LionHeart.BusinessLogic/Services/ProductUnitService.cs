using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;

namespace LionHeart.BusinessLogic.Services;

public class ProductUnitService : IProductUnitService
{
    private readonly IProductUnitRepository _repository;

    public ProductUnitService(IProductUnitRepository repository)
    {
        _repository = repository;
    }

    public Task<ProductUnit?> GetById(string id)
    {
        return _repository.GetById(id);
    }
    public Task<List<ProductUnit>> GetByProductId(string productId, int quantity)
    {
        return _repository.GetByProductId(productId, quantity);
    }
    public Task<List<ProductUnit>> GetAll()
    {
        return _repository.GetAll();
    }
    public Task<int> Add(ProductUnit product)
    {
        return _repository.Add(product);
    }
    public Task<int> Update(ProductUnit product)
    {
        return _repository.Update(product);
    }
    public Task<int> Remove(ProductUnit product)
    {
        return _repository.Remove(product);
    }
    public Task<int> CountByProductId(string productId)
    {
        return _repository.CountByProductId(productId);
    }
}