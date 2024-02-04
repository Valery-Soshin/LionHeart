using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly IProductDetailService _productDetailService;

	public OrderService(IOrderRepository orderRepository,
						IProductDetailService productDetailService)
    {
		_orderRepository = orderRepository;
		_productDetailService = productDetailService;
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
		var productQuantity = await _productDetailService.CountByProductId(order.Product.Id);

		if (productQuantity < order.Quantity) return -1;

		var productDetails = await _productDetailService.GetByProductId(order.Product.Id, order.Quantity);

		if (!productDetails.Any()) return -1;

		foreach (var productDetail in productDetails)
		{
			order.OrderDetails.Add(new OrderDetail
			{
				OrderId = order.Id,
				ProductDetail = productDetail
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