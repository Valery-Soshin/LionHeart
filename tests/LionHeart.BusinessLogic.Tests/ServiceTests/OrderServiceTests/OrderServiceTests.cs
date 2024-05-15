using LionHeart.BusinessLogic.FluentValidations.Validators.Order;
using LionHeart.BusinessLogic.Tests.Factories.Validators;
using LionHeart.BusinessLogic.Services;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using FluentAssertions;
using AutoFixture;
using Moq;
using AutoFixture.Xunit2;
using LionHeart.Core.Dtos.Order;
using LionHeart.BusinessLogic.Tests.Factories;
using LionHeart.BusinessLogic.Helpers;

namespace LionHeart.BusinessLogic.Tests.ServiceTests.OrderServiceTests;

public abstract class OrderServiceTestsBase
{
    protected readonly Fixture _fixture;
    protected readonly Mock<IOrderRepository> _orderRepositoryMock;
    protected readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
    protected readonly Mock<IProductRepository> _productRepositoryMock;
    protected readonly Mock<IProductUnitRepository> _productUnitRepositoryMock;
    protected readonly Mock<IBasketEntryRepository> _basketEntryRepositoryMock;
    protected readonly Mock<INotificationRepository> _notificationRepositoryMock;
    protected readonly Mock<OrderServiceValidators> _validatorsMock;
    protected readonly OrderService _orderService;

    public OrderServiceTestsBase()
    {
        _fixture = FixtureFactory.Create();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _productUnitRepositoryMock = new Mock<IProductUnitRepository>();
        _basketEntryRepositoryMock = new Mock<IBasketEntryRepository>();
        _notificationRepositoryMock = new Mock<INotificationRepository>();
        _validatorsMock = OrderServiceValidatorsFactory.CreateMock();
        _orderService = new OrderService(UnitOfWorkFactory.Create(),
                                         _orderRepositoryMock.Object,
                                         _orderItemRepositoryMock.Object,
                                         _productRepositoryMock.Object,
                                         _productUnitRepositoryMock.Object,
                                         _basketEntryRepositoryMock.Object,
                                         _notificationRepositoryMock.Object,
                                         _validatorsMock.Object);
    }

    protected Order CreateOrder()
    {
        var orderId = _fixture.Create<string>();

        var orderItems = new List<OrderItem>()
        {
            CreateOrderItem(orderId),
            CreateOrderItem(orderId)
        };

        var order = _fixture.Build<Order>()
            .With(o => o.Id, orderId)
            .With(o => o.Items, orderItems)
            .Create();

        return order;
    }
    private OrderItem CreateOrderItem(string orderId)
    {
        var orderItemId = _fixture.Create<string>();

        var orderItemDetails = _fixture.Build<OrderItemDetail>()
            .With(d => d.OrderItemId, orderItemId)
            .CreateMany()
            .ToList();

        var orderItem = _fixture.Build<OrderItem>()
            .Without(i => i.Order)
            .Without(i => i.Product)
            .With(i => i.OrderId, orderId)
            .With(i => i.Details, orderItemDetails)
            .Create();

        return orderItem;
    }
}
public class GetById : OrderServiceTestsBase
{
    [Fact]
    public async Task ShouldReturnsSuccessResult_WhenOrderExists()
    {
        // Arrange

        var order = CreateOrder();

        _orderRepositoryMock.Setup(m => m.GetById(order.Id))
            .ReturnsAsync(order);

        // Act

        var orderServiceResult = await _orderService.GetById(order.Id);

        // Assert

        orderServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenOrderDoesNotExist(string orderId)
    {
        // Arrange

        _orderRepositoryMock.Setup(m => m.GetById(orderId))
            .ReturnsAsync(null as Order);

        // Act

        var orderServiceResult = await _orderService.GetById(orderId);

        // Assert

        orderServiceResult.IsFaulted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenMethodThrowsException(string orderId)
    {
        // Arrange

        _orderRepositoryMock.Setup(m => m.GetById(orderId))
            .ThrowsAsync(new Exception());

        // Act

        var orderServiceResult = await _orderService.GetById(orderId);

        // Assert

        orderServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class GetOrdersByUserId : OrderServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenOrdersExist(string userId)
    {
        // Arrange

        var orders = new List<Order> { CreateOrder(), CreateOrder() };
        var pagedResponse = new PagedResponse<Order>(orders, 1, 2);

        _orderRepositoryMock
            .Setup(m => m.GetOrdersByFilter(
                pagedResponse.PageNumber, PageHelper.PageSize, o => o.UserId == userId))
            .ReturnsAsync(new PagedResponse<Order>(orders, pagedResponse.PageNumber, 1));

        // Act

        var orderServiceResult = await _orderService.GetOrdersByUserId(userId,
            pagedResponse.PageNumber);

        // Assert

        orderServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenMethodThrowsException(string userId)
    {
        // Arrange

        int pageNumber = 1;
        _orderRepositoryMock
            .Setup(m => m.GetOrdersByFilter(pageNumber, PageHelper.PageSize, o => o.UserId == userId))
            .ThrowsAsync(new Exception());

        // Act

        var orderServiceResult = await _orderService.GetOrdersByUserId(userId, pageNumber);

        // Assert

        orderServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class GetOrderItemsByUserId : OrderServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenMethodIsCompleted(string userId)
    {
        // Arrange

        var orders = new List<Order> { CreateOrder(), CreateOrder() };
        var orderItems = orders.SelectMany(o => o.Items).ToList();
        var pagedResponse = new PagedResponse<OrderItem>(orderItems, 1, 1);

        _orderItemRepositoryMock
            .Setup(m => m.GetOrderItemsByFilter(
                pagedResponse.PageNumber, PageHelper.PageSize, i => i.Order.UserId == userId))
            .ReturnsAsync(pagedResponse);

        // Act

        var orderServiceResult = await _orderService
            .GetOrderItemsByUserId(userId, pagedResponse.PageNumber);

        // Assert

        orderServiceResult.IsCompleted.Should().BeTrue();
    }
    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenMethodThrowsException(string userId)
    {
        // Arrange

        int pageNumber = 1;
        _orderItemRepositoryMock
            .Setup(m => m.GetOrderItemsByFilter(
                pageNumber, PageHelper.PageSize, i => i.Order.UserId == userId))
            .ThrowsAsync(new Exception());

        // Act

        var orderServiceResult = await _orderService.GetOrderItemsByUserId(userId, pageNumber);

        // Assert

        orderServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class Add : OrderServiceTestsBase
{
    private List<Product> CreateProducts(List<string> productIds)
    {
        var products = new List<Product>();
        foreach (var productId in productIds)
        {
            products.Add(CreateProduct(productId));
        }
        return products;

        Product CreateProduct(string productId)
        {
            var productUnits = _fixture.Build<ProductUnit>()
                .With(u => u.ProductId, productId)
                .CreateMany()
                .ToList();

            var product = _fixture.Build<Product>()
                .Without(p => p.Category)
                .Without(p => p.Brand)
                .Without(p => p.Company)
                .Without(p => p.Image)
                .Without(p => p.Feedbacks)
                .With(p => p.Id, productId)
                .With(p => p.Units, productUnits)
                .Create();

            return product;
        }
    }
    private List<BasketEntry> CreateBasketEntries(List<string> basketEntryIds)
    {
        var basketEntries = new List<BasketEntry>();
        foreach (var basketEntryId in basketEntryIds)
        {
            basketEntries.Add(CreateBasketEntry(basketEntryId));
        }
        return basketEntries;

        BasketEntry CreateBasketEntry(string basketEntryId)
        {
            var basketEntry = _fixture.Build<BasketEntry>()
                .Without(e => e.Product)
                .Create();

            return basketEntry;
        }
    }
    private void ConfigureAdd(AddOrderDto dto)
    {
        dto.Products.ForEach(p => p.ProductQuantity = 3);

        var productIds = dto.Products.Select(p => p.ProductId).ToList();
        _productRepositoryMock.Setup(m => m.FindProducts(productIds))
            .ReturnsAsync(CreateProducts(productIds));

        var basketEntryIds = dto.Products.Select(p => p.EntryId).ToList();
        var basketEntries = CreateBasketEntries(basketEntryIds);
        _basketEntryRepositoryMock.Setup(m => m.Find(basketEntryIds))
            .ReturnsAsync(basketEntries);

        _basketEntryRepositoryMock.Setup(m => m.RemoveRange(basketEntries))
            .ReturnsAsync(1);
        _productUnitRepositoryMock.Setup(m => m.UpdateRange(It.IsAny<List<ProductUnit>>()))
            .ReturnsAsync(1);
        _notificationRepositoryMock.Setup(m => m.AddRange(It.IsAny<List<Notification>>()))
            .ReturnsAsync(1);
        _orderRepositoryMock.Setup(m => m.Add(It.IsAny<Order>()))
            .ReturnsAsync(1);
    }

    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenAddingIsCompleted(AddOrderDto dto)
    {
        // Arrange

        ConfigureAdd(dto);

        // Act

        var orderServiceResult = await _orderService.Add(dto);

        // Assert

        orderServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsCorrectOrder_WhenAddingIsCompleted(AddOrderDto dto)
    {
        // Arrange

        ConfigureAdd(dto);

        // Act

        var orderServiceResult = await _orderService.Add(dto);
        var order = orderServiceResult.Value;

        // Assert

        order.Items.Count.Should().Be(3);
        order.Items.TrueForAll(i => i.Details.Count == 3).Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenRepositoryThrowsException(AddOrderDto dto)
    {
        // Arrange

        ConfigureAdd(dto);

        _orderRepositoryMock.Setup(m => m.Add(It.IsAny<Order>()))
            .ThrowsAsync(new Exception());

        // Act

        var orderServiceResult = await _orderService.Add(dto);

        // Assert

        orderServiceResult.IsFaulted.Should().BeTrue();
    }

}
public class Any : OrderServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenMethodIsCompleted(string userId)
    {
        // Arrange

        _orderRepositoryMock.Setup(m => m.Any(userId))
            .ReturnsAsync(true);

        // Act

        var orderServiceResult = await _orderService.Any(userId);

        // Assert

        orderServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenRepositoryThrowsException(string userId)
    {
        // Arrange

        _orderRepositoryMock.Setup(m => m.Any(userId))
            .ThrowsAsync(new Exception());

        // Act

        var orderServiceResult = await _orderService.Any(userId);

        // Assert

        orderServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class Exists : OrderServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldResultsSuccessResult_WhenMethodIsCompleted(string userId, string productId)
    {
        // Arrange

        _orderRepositoryMock.Setup(m => m.Exists(userId, productId))
            .ReturnsAsync(true);

        // Act

        var orderServiceResult = await _orderService.Exists(userId, productId);

        // Assert

        orderServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldResultsFailureResult_WhenRepositoryThrowsException(string userId, string productId)
    {
        // Arrange

        _orderRepositoryMock.Setup(m => m.Exists(userId, productId))
            .ThrowsAsync(new Exception());

        // Act

        var orderServiceResult = await _orderService.Exists(userId, productId);

        // Assert

        orderServiceResult.IsFaulted.Should().BeTrue();
    }
}