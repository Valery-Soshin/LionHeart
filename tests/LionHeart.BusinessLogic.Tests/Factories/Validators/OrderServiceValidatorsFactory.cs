using FluentValidation;
using FluentValidation.Results;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Order;
using LionHeart.Core.Dtos.Order;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Order;
using Moq;

namespace LionHeart.BusinessLogic.Tests.Factories.Validators;

public static class OrderServiceValidatorsFactory
{
    public static OrderServiceValidators Create()
    {
        return CreateMock().Object;
    }
    public static Mock<OrderServiceValidators> CreateMock()
    {
        var mock = new Mock<OrderServiceValidators>();

        mock = new Mock<OrderServiceValidators>(
            new Mock<IOrderValidator>().Object,
            new Mock<IValidator<AddOrderDto>>().Object,
            new Mock<IValidator<IdModel>>().Object);

        mock.Setup(m => m.OrderValidator.ValidateAdd(It.IsAny<ValidateAddModel>()))
            .Returns(Result.Success());

        mock.Setup(m => m.AddOrderDtoValidator.Validate(It.IsAny<AddOrderDto>()))
            .Returns(new ValidationResult());

        mock.Setup(m => m.IdValidator.Validate(It.IsAny<IdModel>()))
            .Returns(new ValidationResult());

        return mock;
    }
}