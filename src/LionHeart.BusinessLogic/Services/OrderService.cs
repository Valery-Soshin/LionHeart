using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Orders;
using LionHeart.Core.Enums;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;
using System.Xml;

namespace LionHeart.BusinessLogic.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository,
                        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<Order>> GetById(string id)
    {
        try
        {
            var order = await _orderRepository.GetById(id);
            if (order is null)
            {
                return new Result<Order>()
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.OrderNotFound
                };
            }
            return new Result<Order>()
            {
                IsCompleted = true,
                Data = order
            };
        }
        catch
        {
            return new Result<Order>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<Order>>> GetOrdersByUserId(string userId)
    {
        try
        {
            var orders = await _orderRepository.GetOrdersByUserId(userId);
            if (orders is null)
            {
                return new Result<List<Order>>()
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.OrdersNotFound
                };
            }
            return new Result<List<Order>>()
            {
                IsCompleted = true,
                Data = orders
            };
        }
        catch
        {
            return new Result<List<Order>>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<Order>>> GetAll()
    {
        try
        {
            var orders = await _orderRepository.GetAll();
            if (orders is null)
            {
                return new Result<List<Order>>()
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.OrdersNotFound
                };
            }
            return new Result<List<Order>>()
            {
                IsCompleted = true,
                Data = orders
            };
        }
        catch
        {
            return new Result<List<Order>>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Order>> Add(AddOrderDto dto)
    {
        try
        {
            var order = new Order
            {
                UserId = dto.UserId,
                TotalPrice = dto.BasketTotalPrice,
                CreateAt = dto.CreateAt
            };

            var products = await _productRepository.GetAll(
                dto.Products.Select(p => p.ProductId).ToList());

            if (products is null)
            {
                return new Result<Order>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotFound
                };
            }
            if (products.Exists(p => p.Units.Count < dto.Products.Single(d => d.ProductId == p.Id).ProductQuantity))
            {
                return new Result<Order>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotEnough
                };
            }
            foreach (var productDto in dto.Products)
            {
                var product = products.Single(p => p.Id == productDto.ProductId);
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    ProductPrice = product.Price,
                    ProductQuantity = productDto.ProductQuantity,
                };
                for (int i = 0; i < productDto.ProductQuantity; i++)
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
            }
            await _productRepository.UpdateRange(products);
            await _orderRepository.Add(order);
            return new Result<Order>()
            {
                IsCompleted = true,
                Data = order
            };
        }
        catch
        {
            return new Result<Order>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<bool>> Any(string userId)
    {
        try
        {
            return new Result<bool>()
            {
                IsCompleted = true,
                Data = await _orderRepository.Any(userId)
            };
        }
        catch
        {
            return new Result<bool>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<bool>> Exists(string userId, string productId)
    {
        try
        {
            return new Result<bool>()
            {
                IsCompleted = true,
                Data = await _orderRepository.Exists(userId, productId)
            };
        }
        catch
        {
            return new Result<bool>()
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}