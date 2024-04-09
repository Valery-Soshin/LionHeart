using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Notification;
using LionHeart.Core.Dtos.Order;
using LionHeart.Core.Dtos.ProductUnit;
using LionHeart.Core.Enums;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductService _productService;
    private readonly IProductUnitService _productUnitService;
    private readonly IBasketEntryService _basketEntryService;
    private readonly INotificationService _notificationService;

    public OrderService(IUnitOfWork unitOfWork,
                        IOrderRepository orderRepository,
                        IProductService productService,
                        IProductUnitService productUnitService,
                        IBasketEntryService basketEntryService,
                        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _productService = productService;
        _productUnitService = productUnitService;
        _basketEntryService = basketEntryService;
        _notificationService = notificationService;
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
            await _unitOfWork.BeginTransaction();

            var productServiceResult = await _productService.GetAll(
                dto.Products.Select(p => p.ProductId).ToList());

            if (productServiceResult.IsFaulted || productServiceResult.Data is null)
            {
                return new Result<Order>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotFound
                };
            }

            var products = productServiceResult.Data;

            bool areEnoughProducts = products.Exists(
                p => p.Units.Count < dto.Products.Single(d => d.ProductId == p.Id).ProductQuantity);

            if (areEnoughProducts)
            {
                return new Result<Order>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotEnough
                };
            }

            var order = new Order
            {
                UserId = dto.UserId,
                TotalPrice = dto.BasketTotalPrice,
                CreateAt = dto.CreateAt
            };
            var productUnits = new List<ProductUnit>();
            foreach (var product in products)
            {
                var productQuantity = dto.Products
                    .Single(p => p.ProductId == product.Id)
                    .ProductQuantity;

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    ProductPrice = product.Price,
                    ProductQuantity = productQuantity,
                };
                for (int i = 0; i < productQuantity; i++)
                {
                    var productUnit = product.Units[i];
                    productUnit.SaleStatus = SaleStatus.Sold;
                    productUnits.Add(productUnit);

                    orderItem.Details.Add(new OrderItemDetail
                    {
                        OrderItemId = order.Id,
                        ProductUnitId = productUnit.Id
                    });
                }
                order.Items.Add(orderItem);
            }
            await _orderRepository.Add(order);

            var productUnitDtos = productUnits.Select(p => new UpdateProductUnitDto
            {
                Id = p.Id,
                SaleStatus = p.SaleStatus
            }).ToList();
            var productUnitServiceResult = await _productUnitService.UpdateRange(productUnitDtos);

            var entriesIds = dto.Products.Select(p => p.EntryId)
                .ToList();
            var basketEntryServiceResult = await _basketEntryService.RemoveRange(entriesIds);

            var notificationDto = new AddNotificationDto()
            { 
                UserId = dto.UserId,
                Content = NotificationMessage.OrderCreated
            };
            var notificationServiceResult = await _notificationService.Add(notificationDto);

            if (productUnitServiceResult.IsFaulted ||
                basketEntryServiceResult.IsFaulted ||
                notificationServiceResult.IsFaulted)
            {
                await _unitOfWork.Rollback();
                return new Result<Order>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.InternalServerError
                };
            }

            await _unitOfWork.Commit();
            return new Result<Order>()
            {
                IsCompleted = true,
                Data = order
            };
        }
        catch
        {
            await _unitOfWork.Rollback();
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