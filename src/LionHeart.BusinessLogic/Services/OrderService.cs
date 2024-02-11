using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;

	public OrderService(IOrderRepository orderRepository)
    {
		_orderRepository = orderRepository;
    }

    public Task<Order?> GetById(string id)
	{
		return _orderRepository.GetById(id);
	}
	public Task<List<Order>> GetAll()
	{
		return _orderRepository.GetAll();
	}
	public Task<int> Add(Order order)
	{
		return _orderRepository.Add(order);
	}
	public Task<int> Update(Order order)
	{
		return _orderRepository.Update(order);
	}
	public Task<int> Remove(Order order)
	{
		return _orderRepository.Remove(order);
	}
}