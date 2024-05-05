using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.Order;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Order;

public class OrderServiceValidators
{
    public IOrderValidator OrderValidator { get; }
    public IValidator<AddOrderDto> AddOrderDtoValidator { get; }
    public IValidator<IdModel> IdValidator { get; }

    public OrderServiceValidators(IOrderValidator orderValidator,
                                  IValidator<AddOrderDto> addOrderDtoValidator,
                                  IValidator<IdModel> idValidator)
    {
        OrderValidator = orderValidator;
        AddOrderDtoValidator = addOrderDtoValidator;
        IdValidator = idValidator;
    }
}