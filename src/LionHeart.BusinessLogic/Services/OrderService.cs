using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository orderRepository)
    {
		_repository = orderRepository;
    }

    public Task<Order?> GetById(string id)
	{
		return _repository.GetById(id);
	}
	public Task<List<Order>> GetOrdersByUserId(string userId)
	{
		return _repository.GetOrdersByUserId(userId);
	}
	public Task<List<Order>> GetAll()
	{
		return _repository.GetAll();
	}
	public Task<int> Add(Order order)
	{
		return _repository.Add(order);
	}
    public Task<int> Update(Order order)
	{
		return _repository.Update(order);
	}
	public Task<int> Remove(Order order)
	{
		return _repository.Remove(order);
	}
	public Task<bool> Any(string userId)
	{
		return _repository.Any(userId);
	}
    public Task<bool> Exists(string userId, string productId)
    {
		return _repository.Exists(userId, productId);
    }
}