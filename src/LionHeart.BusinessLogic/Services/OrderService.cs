using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Order;
using LionHeart.BusinessLogic.Helpers;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Order;
using LionHeart.Core.Enums;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Order;

namespace LionHeart.BusinessLogic.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly IBasketEntryRepository _basketEntryRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly OrderServiceValidators _validators;

    public OrderService(IUnitOfWork unitOfWork,
                        IOrderRepository orderRepository,
                        IOrderItemRepository orderItemRepository,
                        IProductRepository productRepository,
                        IProductUnitRepository productUnitRepository,
                        IBasketEntryRepository basketEntryRepository,
                        INotificationRepository notificationRepository,
                        OrderServiceValidators validators)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _basketEntryRepository = basketEntryRepository;
        _notificationRepository = notificationRepository;
        _validators = validators;
    }

    public async Task<Result<Order>> GetById(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Order>.Failure(errorMessages);
            }

            var order = await _orderRepository.GetById(id);
            if (order is null)
            {
                return Result<Order>.Failure(ErrorMessage.OrderNotFound);
            }
            return Result<Order>.Success(order);
        }
        catch
        {
            return Result<Order>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Order>>> GetOrdersByUserId(string userId, int pageNumber)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<Order>>.Failure(errorMessages);
            }

            var page = await _orderRepository.GetOrdersByFilter(
                pageNumber, PageHelper.PageSize, o => o.UserId == userId);

            return Result<PagedResponse<Order>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Order>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<OrderItem>>> GetOrderItemsByUserId(string userId, int pageNumber)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<OrderItem>>.Failure(errorMessages);
            }

            var page = await _orderItemRepository.GetOrderItemsByFilter(
                pageNumber, PageHelper.PageSize, o => o.Order.UserId == userId);

            return Result<PagedResponse<OrderItem>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<OrderItem>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Order>> Add(AddOrderDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.AddOrderDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Order>.Failure(errorMessages);
            }

            var productIds = dto.Products.Select(p => p.ProductId).ToList();
            var entryIds = dto.Products.Select(p => p.EntryId).ToList();
            var products = await _productRepository.FindProducts(productIds);
            
            var validateAddModel = new ValidateAddModel()
            {
                FoundDtoProductsInDb = products,
                DtoProducts = dto.Products
            };
            var orderValidatorResult = _validators.OrderValidator.ValidateAdd(validateAddModel);
            if (orderValidatorResult.IsFaulted)
            {
                return Result<Order>.Failure(orderValidatorResult.ErrorMessages);
            }

            await _unitOfWork.BeginTransaction();

            var order = new Order
            {
                UserId = dto.UserId,
                TotalPrice = dto.BasketTotalPrice,
                CreatedAt = dto.CreateAt
            };
            var productUnits = new List<ProductUnit>();
            var notifications = new List<Notification>();
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
                notifications.Add(new Notification()
                {
                    UserId = dto.UserId,
                    Content = NotificationMessage.ProductPurchased,
                    LinkToAction = $"/Feedbacks/CreateFeedback/?productId={product.Id}",
                    CreatedAt = DateTimeOffset.UtcNow
                });
            }
            var basketEntries = await _basketEntryRepository.Find(entryIds);
            var basketEntryServiceResult = await _basketEntryRepository.RemoveRange(basketEntries);
            var productUnitServiceResult = await _productUnitRepository.UpdateRange(productUnits);
            var notificationServiceResult = await _notificationRepository.AddRange(notifications);
            var orderRepositoryResult = await _orderRepository.Add(order);

            if (basketEntryServiceResult <= 0 ||
                productUnitServiceResult <= 0 ||
                notificationServiceResult <= 0 ||
                orderRepositoryResult <= 0)
            {
                await _unitOfWork.Rollback();
                return Result<Order>.Failure(ErrorMessage.InternalServerError);
            }
            await _unitOfWork.Commit();
            return Result<Order>.Success(order);
        }
        catch
        {
            await _unitOfWork.Rollback();
            return Result<Order>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<bool>> Any(string userId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<bool>.Failure(errorMessages);
            }

            bool orderAny = await _orderRepository.Any(userId);
            return Result<bool>.Success(orderAny);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<bool>> Exists(string userId, string productId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId, productId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<bool>.Failure(errorMessages);
            }

            bool orderExists = await _orderRepository.Exists(userId, productId);
            return Result<bool>.Success(orderExists);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
}