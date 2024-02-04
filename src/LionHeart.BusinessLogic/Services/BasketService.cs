using LionHeart.Core.Enums;
using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _repository;

    public BasketService(IBasketRepository repository)
    {
        _repository = repository;
    }

    public Task<Basket?> GetById(string id)
    {
        return _repository.GetById(id);
    }
    public Task<Basket?> GetByCustomerId(string customerId)
    {
        return _repository.GetByCustomerId(customerId);
    }
    public Task<List<Basket>> GetAll()
    {
        return _repository.GetAll();
    }
    public Task<int> Add(Basket product)
    {
        return _repository.Add(product);
    }
    public Task<int> Update(Basket product)
    {
        return _repository.Update(product);
    }
    public Task<int> Remove(Basket product)
    {
        return _repository.Remove(product);
    }
    public Task<bool> HasProduct(string customerId, string productId)
    {
        return _repository.HasProduct(customerId, productId);
    }
}