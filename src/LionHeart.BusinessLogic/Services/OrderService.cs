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
            //var orders = await _orderRepository.GetAll();
            var orders = new List<Order>();
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

            var productServiceResult = await _productService.GetProductsByIds(
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

            bool allProductsFound = products.Count == dto.Products.Count;
            bool areEnoughProducts = products.Exists(
                p => p.Units.Count < dto.Products.Single(d => d.ProductId == p.Id).ProductQuantity);

            if (areEnoughProducts && allProductsFound)
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
            var productUnitDtos = new List<UpdateProductUnitDto>();
            var notificationDtos = new List<AddNotificationDto>();
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
                    productUnitDtos.Add(new UpdateProductUnitDto
                    {
                        Id = productUnit.Id,
                        SaleStatus = productUnit.SaleStatus
                    });
                    orderItem.Details.Add(new OrderItemDetail
                    {
                        OrderItemId = order.Id,
                        ProductUnitId = productUnit.Id
                    });
                }
                order.Items.Add(orderItem);
                notificationDtos.Add(new AddNotificationDto()
                {
                    UserId = dto.UserId,
                    ProductId = product.Id,
                    Content = NotificationMessage.ProductPurchased,
                    LinkToAction = $"/Feedbacks/CreateFeedback/?productId={product.Id}",
                    CreatedAt = DateTimeOffset.UtcNow
                });
            }
            var basketEntryServiceResult = await _basketEntryService.RemoveRange(
                dto.Products.Select(p => p.EntryId).ToList());
            var productUnitServiceResult = await _productUnitService.UpdateRange(productUnitDtos);
            var notificationServiceResult = await _notificationService.AddRange(notificationDtos);
            var orderRepositoryResult = await _orderRepository.Add(order);

            if (basketEntryServiceResult.IsFaulted || 
                productUnitServiceResult.IsFaulted ||
                notificationServiceResult.IsFaulted || 
                orderRepositoryResult <= 0)
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