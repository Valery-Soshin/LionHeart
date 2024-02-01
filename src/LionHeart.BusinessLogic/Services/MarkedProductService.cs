using LionHeart.Core.Enums;
using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class MarkedProductService : IMarkedProductService
{
    private readonly IMarkedProductRepository _repository;

    public MarkedProductService(IMarkedProductRepository repository)
    {
        _repository = repository;
    }

    public Task<MarkedProduct?> GetById(string id)
    {
        return _repository.GetById(id);
    }
    public Task<MarkedProduct?> GetByCustomerIdProductId(string customerId, string productId, Mark mark)
    {
        return _repository.GetByCustomerIdProductId(customerId, productId, mark);
    }
    public Task<List<MarkedProduct>> GetAll()
    {
        return _repository.GetAll();
    }
    public Task<List<MarkedProduct>> GetAllByCustomerId(string customerId, Mark mark)
    {
        return _repository.GetAllByCustomerId(customerId, mark);
    }
    public Task<int> Add(MarkedProduct product)
    {
        return _repository.Add(product);
    }
    public Task<int> Update(MarkedProduct product)
    {
        return _repository.Update(product);
    }
    public Task<int> Remove(MarkedProduct product)
    {
        return _repository.Remove(product);
    }
}