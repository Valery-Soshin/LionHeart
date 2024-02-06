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
    public async Task<Basket> GetByCustomerId(string userId)
    {
        var basket = await _repository.GetByCustomerId(userId);

        return await EnsureBasket(userId, basket);
    }
    public Task<List<Basket>> GetAll()
    {
        return _repository.GetAll();
    }
    public Task<int> Add(Basket basket)
    {
        return _repository.Add(basket);
    }
    public Task<int> Update(Basket basket)
    {
        return _repository.Update(basket);
    }
    public Task<int> Remove(Basket basket)
    {
        return _repository.Remove(basket);
    }
    public Task<bool> HasProduct(string userId, string productId)
    {
        return _repository.HasProduct(userId, productId);
    }
    private async Task<Basket> EnsureBasket(string userId, Basket? basket)
    {
        if (basket is null)
        {
            basket = new Basket()
            {
                UserId = userId
            };

            await _repository.Add(basket);
        }

        return basket;
    }
}