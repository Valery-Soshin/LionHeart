using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IBasketService
{
    Task<Basket?> GetById(string id);
	Task<Basket?> GetByCustomerId(string customerId);
	Task<int> Add(Basket product);
    Task<int> Update(Basket product);
    Task<int> Remove(Basket product);
	Task<bool> HasProduct(string customerId, string productId);
}