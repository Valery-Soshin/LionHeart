using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly IProductUnitService _productUnitService;

	public OrderService(IOrderRepository orderRepository,
						IProductUnitService productUnitService)
    {
		_orderRepository = orderRepository;
		_productUnitService = productUnitService;
    }

    public Task<Order?> GetById(string id)
	{
		return _orderRepository.GetById(id);
	}
	public Task<List<Order>> GetAll()
	{
		return _orderRepository.GetAll();
	}
	public async Task<int> Add(Order order)
	{
		var productQuantity = await _productUnitService.CountByProductId(order.Product.Id);

		if (productQuantity < order.Quantity) return -1;

		var productUnits = await _productUnitService.GetByProductId(order.Product.Id, order.Quantity);

		if (!productUnits.Any()) return -1;

		foreach (var productUnit in productUnits)
		{
			order.OrderDetails.Add(new OrderDetail
			{
				OrderId = order.Id,
				ProductUnit = productUnit
			});
		}

		return await _orderRepository.Add(order);
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