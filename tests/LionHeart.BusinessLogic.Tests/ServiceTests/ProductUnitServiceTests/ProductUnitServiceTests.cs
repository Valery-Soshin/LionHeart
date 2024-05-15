using LionHeart.BusinessLogic.Services;
using LionHeart.BusinessLogic.Tests.Factories.Validators;
using LionHeart.Core.Interfaces.Repositories;
using FluentAssertions;
using Moq;
using LionHeart.Core.Models;
using AutoFixture;
using LionHeart.BusinessLogic.Tests.Factories.AutoFixture;
using AutoFixture.Xunit2;
using LionHeart.Core.Dtos.ProductUnit;
using System.Linq.Expressions;
using LionHeart.BusinessLogic.FluentValidations.Validators.ProductUnit;
using FluentValidation.Results;

namespace LionHeart.BusinessLogic.Tests.ServiceTests.ProductUnitServiceTests;

public abstract class ProductUnitServiceTestsBase
{
    protected readonly Fixture _fixture;
    protected readonly Mock<IProductUnitRepository> _productUnitRepositoryMock;
    protected readonly Mock<IProductRepository> _productRepositoryMock;
    protected readonly Mock<ProductUnitServiceValidators> _validators;
    protected readonly ProductUnitService _productUnitService;

    public ProductUnitServiceTestsBase()
    {
        _fixture = FixtureFactory.Create();
        _productUnitRepositoryMock = new Mock<IProductUnitRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _validators = ProductUnitServiceValidatorsFactory.CreateMock();
        _productUnitService = new ProductUnitService(_productUnitRepositoryMock.Object,
                                                     _productRepositoryMock.Object,
                                                     _validators.Object);
    }
}

public class GetById : ProductUnitServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenMethodIsCompleted(Core.Models.ProductUnit productUnit)
    {
        // Arrange

        _productUnitRepositoryMock
            .Setup(m => m.GetById(productUnit.Id))
            .ReturnsAsync(productUnit);

        // Act

        var productUnitServiceResult = await _productUnitService.GetById(productUnit.Id);

        // Assert

        productUnitServiceResult.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldReturnsFailureResult_WhenProductUnitDoesNotExist()
    {
        // Arrange

        _productUnitRepositoryMock.Setup<Task<Core.Models.ProductUnit>>(m => m.GetById("-"))
            .ReturnsAsync<IProductUnitRepository, Core.Models.ProductUnit>(null as Core.Models.ProductUnit);

        // Act

        var productUnitServiceResult = await _productUnitService.GetById("-");

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldReturnsFailureResult_WhenMethodThrowsException()
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.GetById("-"))
            .ThrowsAsync(new Exception());

        // Act

        var productUnitServiceResult = await _productUnitService.GetById("-");

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class Add : ProductUnitServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenAddingIsCompleted(AddProductUnitDto dto)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Add(It.IsAny<ProductUnit>()))
            .ReturnsAsync(1);

        // Act

        var productUnitServiceResult = await _productUnitService.Add(dto);

        // Assert

        productUnitServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenAddingIsFaulted(AddProductUnitDto dto)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Add(It.IsAny<ProductUnit>()))
            .ReturnsAsync(-1);

        // Act

        var productUnitServiceResult = await _productUnitService.Add(dto);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenMethodThrowsException(AddProductUnitDto dto)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Add(It.IsAny<ProductUnit>()))
            .ThrowsAsync(new Exception());

        // Act

        var productUnitServiceResult = await _productUnitService.Add(dto);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class AddRange : ProductUnitServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenAddingIsCompleted(List<AddProductUnitDto> dtos)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.AddRange(It.IsAny<List<ProductUnit>>()))
            .ReturnsAsync(dtos.Count);

        // Act

        var productUnitServiceResult = await _productUnitService.AddRange(dtos);

        // Assert

        productUnitServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenAddingIsFaulted(List<AddProductUnitDto> dtos)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.AddRange(It.IsAny<List<ProductUnit>>()))
            .ReturnsAsync(-1);

        // Act

        var productUnitServiceResult = await _productUnitService.AddRange(dtos);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenMethodThrowsException(List<AddProductUnitDto> dtos)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.AddRange(It.IsAny<List<ProductUnit>>()))
            .ThrowsAsync(new Exception());

        // Act

        var productUnitServiceResult = await _productUnitService.AddRange(dtos);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class Update : ProductUnitServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenUpdatingIsCompleted(UpdateProductUnitDto dto, Core.Models.ProductUnit productUnit)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Exists(u => u.Id == dto.Id))
            .ReturnsAsync(true);

        _productUnitRepositoryMock.Setup(m => m.GetById(dto.Id))
            .ReturnsAsync(productUnit);

        _productUnitRepositoryMock.Setup(m => m.Update(productUnit))
            .ReturnsAsync(1);

        // Act

        var productUnitServiceResult = await _productUnitService.Update(dto);

        // Assert

        productUnitServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenProductUnitDoesNotExist(UpdateProductUnitDto dto)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Exists(u => u.Id == dto.Id))
            .ReturnsAsync(false);

        // Act

        var productUnitServiceResult = await _productUnitService.Update(dto);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenMethodThrowsException(UpdateProductUnitDto dto, Core.Models.ProductUnit productUnit)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Exists(u => u.Id == dto.Id))
            .ReturnsAsync(true);

        _productUnitRepositoryMock.Setup(m => m.GetById(dto.Id))
            .ReturnsAsync(productUnit);

        _productUnitRepositoryMock.Setup(m => m.Update(productUnit))
            .ThrowsAsync(new Exception());

        // Act

        var productUnitServiceResult = await _productUnitService.Update(dto);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class UpdateRange : ProductUnitServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenUpdatingIsCompleted(List<UpdateProductUnitDto> dtos,
                                                                         List<ProductUnit> productUnits)
    {
        // Arrange

        dtos.ReplacePropertyValue(productUnits, (dto, unit) => dto.Id = unit.Id);

        _productUnitRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<ProductUnit, bool>>>()))
                .ReturnsAsync(true);

        _productUnitRepositoryMock.Setup(m => m.FindProductUnits(It.IsAny<List<string>>()))
            .ReturnsAsync(productUnits);

        _productUnitRepositoryMock.Setup(m => m.UpdateRange(productUnits))
            .ReturnsAsync(productUnits.Count);

        // Act

        var productUnitServiceResult = await _productUnitService.UpdateRange(dtos);

        // Assert

        productUnitServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenProductUnitDoesNotExist(List<UpdateProductUnitDto> dtos)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<ProductUnit, bool>>>()))
                .ReturnsAsync(false);

        _validators.Setup(m => m.UpdateProductUnitValidator.Validate(It.IsAny<UpdateProductUnitDto>()))
            .Returns(new ValidationResult() { Errors = new List<ValidationFailure>(1) });

        // Act

        var productUnitServiceResult = await _productUnitService.UpdateRange(dtos);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenValidatorThrowsException(List<UpdateProductUnitDto> dtos)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<ProductUnit, bool>>>()))
                .ReturnsAsync(true);

        _validators.Setup(m => m.UpdateProductUnitValidator.Validate(It.IsAny<UpdateProductUnitDto>()))
            .Throws(new Exception());

        // Act

        var productUnitServiceResult = await _productUnitService.UpdateRange(dtos);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenRepositoryThrowsException(List<UpdateProductUnitDto> dtos,
                                                                               List<ProductUnit> productUnits)
    {
        // Arrange

        dtos.ReplacePropertyValue(productUnits, (dto, unit) => dto.Id = unit.Id);

        _productUnitRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<ProductUnit, bool>>>()))
                .ReturnsAsync(true);

        _productUnitRepositoryMock.Setup(m => m.FindProductUnits(It.IsAny<List<string>>()))
            .ReturnsAsync(productUnits);

        _productUnitRepositoryMock.Setup(m => m.UpdateRange(productUnits))
            .ThrowsAsync(new Exception());

        // Act

        var productUnitServiceResult = await _productUnitService.UpdateRange(dtos);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class Remove : ProductUnitServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenProductUnitExists(string productUnitId, Core.Models.ProductUnit productUnit)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.GetById(productUnitId))
            .ReturnsAsync(productUnit);

        _productUnitRepositoryMock.Setup(m => m.Remove(productUnit))
            .ReturnsAsync(1);

        // Act

        var productUnitServiceResult = await _productUnitService.Remove(productUnitId);

        // Assert

        productUnitServiceResult.IsCompleted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenProductUnitDoesNotExist(string productUnitId)
    {
        // Arrange

        _productUnitRepositoryMock.Setup<Task<Core.Models.ProductUnit>>(m => m.GetById(productUnitId))
            .ReturnsAsync<IProductUnitRepository, Core.Models.ProductUnit>(null as Core.Models.ProductUnit);

        // Act

        var productUnitServiceResult = await _productUnitService.Remove(productUnitId);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenMethodThrowsException(string productUnitId, Core.Models.ProductUnit productUnit)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.GetById(productUnitId))
            .ReturnsAsync(productUnit);

        _productUnitRepositoryMock.Setup(m => m.Remove(productUnit))
            .ThrowsAsync(new Exception());

        // Act

        var productUnitServiceResult = await _productUnitService.Remove(productUnitId);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }
}
public class Count : ProductUnitServiceTestsBase
{
    [Theory, AutoData]
    public async Task ShouldReturnsSuccessResult_WhenProductUnitsExist(string productId)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Count(productId))
            .ReturnsAsync(5);

        // Act

        var productUnitServiceResult = await _productUnitService.Count(productId);

        // Assert

        productUnitServiceResult.Value.Should().Be(5);
    }

    [Theory, AutoData]
    public async Task ShouldReturnsFailureResult_WhenMethodThrowsException(string productId)
    {
        // Arrange

        _productUnitRepositoryMock.Setup(m => m.Count(productId))
            .ThrowsAsync(new Exception());

        // Act

        var productUnitServiceResult = await _productUnitService.Count(productId);

        // Assert

        productUnitServiceResult.IsFaulted.Should().BeTrue();
    }
}