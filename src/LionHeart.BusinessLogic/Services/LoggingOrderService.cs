using LionHeart.Core.Models;
using LionHeart.Core.Services;
using Microsoft.Extensions.Logging;

namespace LionHeart.BusinessLogic.Services;

public class LoggingOrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly OrderService _orderService;

    public LoggingOrderService(OrderService orderService,
                               ILogger<OrderService> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public Task<Order?> GetById(string id)
    {
        _logger.LogInformation("Order with ID '{id}' has been received", id);
        return _orderService.GetById(id);
    }
    public Task<List<Order>> GetOrdersByUserId(string userId)
    {
        _logger.LogInformation("Orders were received by user ID '{userId}'", userId);
        return _orderService.GetOrdersByUserId(userId);
    }
    public Task<List<Order>> GetAll()
    {
        _logger.LogInformation("All orders have been received");
        return _orderService.GetAll();
    }
    public Task<int> Add(Order order)
    {
        _logger.LogInformation(
            "Order with user ID '{userId}' and total price '{totalPrice}' has been added",
            order.UserId,
            order.TotalPrice);

        return _orderService.Add(order);
    }
    public Task<int> Update(Order order)
    {
        _logger.LogInformation("Order '{id}' has been updated", order.Id);
        return _orderService.Update(order);
    }
    public Task<int> Remove(Order order)
    {
        _logger.LogInformation("Order '{id}' has been deleted", order.Id);
        return _orderService.Remove(order);
    }
    public Task<bool> Any(string userId)
    {
        return _orderService.Any(userId);
    }
}