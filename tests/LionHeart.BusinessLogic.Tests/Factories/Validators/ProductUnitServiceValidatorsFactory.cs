using FluentValidation;
using FluentValidation.Results;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.ProductUnit;
using LionHeart.Core.Dtos.ProductUnit;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.ProductUnit;
using Moq;

namespace LionHeart.BusinessLogic.Tests.Factories.Validators;

public class ProductUnitServiceValidatorsFactory
{
    public static ProductUnitServiceValidators Create()
    {
        return CreateMock().Object;
    }
    public static Mock<ProductUnitServiceValidators> CreateMock()
    {
        var mock = new Mock<ProductUnitServiceValidators>(
            new Mock<IProductUnitValidator>().Object,
            new Mock<IValidator<AddProductUnitDto>>().Object,
            new Mock<IValidator<UpdateProductUnitDto>>().Object,
            new Mock<IValidator<IdModel>>().Object);

        mock.Setup(m => m.ProductUnitValidator.ValidateAdd(It.IsAny<ValidateAddModel>()))
            .Returns(Result.Success());

        mock.Setup(m => m.ProductUnitValidator.ValidateUpdate(It.IsAny<ValidateUpdateModel>()))
            .Returns(Result.Success());

        mock.Setup(m => m.AddProductUnitDtoVadidator.Validate(It.IsAny<AddProductUnitDto>()))
            .Returns(new ValidationResult());

        mock.Setup(m => m.UpdateProductUnitValidator.Validate(It.IsAny<UpdateProductUnitDto>()))
            .Returns(new ValidationResult());

        mock.Setup(m => m.IdValidator.Validate(It.IsAny<IdModel>()))
            .Returns(new ValidationResult());

        return mock;
    }
}