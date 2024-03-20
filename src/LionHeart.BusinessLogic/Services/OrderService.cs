using LionHeart.Core.Dtos.Orders;
using LionHeart.Core.Enums;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Response;
using Microsoft.Extensions.Logging;

namespace LionHeart.BusinessLogic.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductService _productService;
    private readonly IBasketEntryService _basketEntryService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository,
                        IProductService productService,
                        IBasketEntryService basketEntryService,
                        ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _productService = productService;
        _basketEntryService = basketEntryService;
        _logger = logger;
    }

    public async Task<IBaseResponse<Order>> GetById(string id)
    {
        try
        {
            var order = await _orderRepository.GetById(id);
            if (order is null)
            {
                return new BaseResponse<Order>()
                {
                    IsCompleted = false,
                    Description = $"The order with ID '{id}' has not been received"
                };
            }

            _logger.LogInformation("The order with ID '{id}' has been received", id);
            return new BaseResponse<Order>()
            {
                IsCompleted = true,
                Description = $"The order with ID '{id}' has been received",
                Data = order
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The order with ID '{id}' has not been: {ex}", id, ex.Message);
            return new BaseResponse<Order>()
            {
                IsCompleted = false,
                Description = $"The order with ID '{id}' has been received: {ex.Message}"
            };
        }
    }
    public async Task<IBaseResponse<List<Order>>> GetOrdersByUserId(string userId)
    {
        try
        {
            var orders = await _orderRepository.GetOrdersByUserId(userId);
            if (orders is null)
            {
                return new BaseResponse<List<Order>>()
                {
                    IsCompleted = false,
                    Description = $"Orders by user ID '{userId}' have not been received"
                };
            }

            _logger.LogInformation("Orders by user ID '{userId}' have been received", userId);
            return new BaseResponse<List<Order>>()
            {
                IsCompleted = true,
                Description = $"Orders by user ID '{userId}' have been received",
                Data = orders
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Orders by user ID '{userId}' have not been: {ex}", userId, ex.Message);
            return new BaseResponse<List<Order>>()
            {
                IsCompleted = false,
                Description = $"Orders by user ID '{userId}' have not been received: {ex.Message}"
            };
        }
    }
    public async Task<IBaseResponse<List<Order>>> GetAll()
    {
        try
        {
            var orders = await _orderRepository.GetAll();
            if (orders is null)
            {
                return new BaseResponse<List<Order>>()
                {
                    IsCompleted = false,
                    Description = $"All orders have not been received"
                };
            }

            _logger.LogInformation("All orders have been received");
            return new BaseResponse<List<Order>>()
            {
                IsCompleted = true,
                Description = $"All orders have been received",
                Data = orders
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "All orders have not been received: {ex}", ex.Message);
            return new BaseResponse<List<Order>>()
            {
                IsCompleted = false,
                Description = $"All orders have not been received: {ex.Message}"
            };
        }
    }
    public async Task<IBaseResponse<Order>> Add(CreateOrderDto model)
    {
        try
        {
            var order = new Order
            {
                UserId = model.UserId,
                TotalPrice = model.BasketTotalPrice,
                CreateAt = DateTimeOffset.UtcNow
            };

            foreach (var entry in model.Entries)
            {
                var product = await _productService.GetById(entry.ProductId);
                if (product is null) continue;
                if (product.Units.Count < entry.ProductQuantity) continue;

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    ProductPrice = product.Price,
                    ProductQuantity = entry.ProductQuantity
                };

                for (int i = 0; i < entry.ProductQuantity; i++)
                {
                    var unit = product.Units[i];
                    orderItem.Details.Add(new OrderItemDetail
                    {
                        OrderItemId = order.Id,
                        ProductUnitId = unit.Id
                    });
                    unit.SaleStatus = SaleStatus.Sold;
                }
                order.Items.Add(orderItem);
                await _productService.Update(product);
                await _basketEntryService.Remove(entry.Id);
            }
            await _orderRepository.Add(order);
            return new BaseResponse<Order>()
            {
                IsCompleted = true,
                Description = "The order has been created",
                Data = order
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Order>()
            {
                IsCompleted = false,
                Description = $"The order has not been created: {ex.Message}"
            };
        }
    }
    public async Task<IBaseResponse<bool>> Any(string userId)
    {
        try
        {
            return new BaseResponse<bool>()
            {
                IsCompleted = true,
                Data = await _orderRepository.Any(userId)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[OrderService.Any]: {message}", ex.Message);
            return new BaseResponse<bool>()
            {
                IsCompleted = false,
            };
        }
    }
    public async Task<IBaseResponse<bool>> Exists(string userId, string productId)
    {
        try
        {
            return new BaseResponse<bool>()
            {
                IsCompleted = true,
                Data = await _orderRepository.Exists(userId, productId)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[OrderService.Exists]: {message}", ex.Message);
            return new BaseResponse<bool>()
            {
                IsCompleted = false,
            };
        }
    }
}