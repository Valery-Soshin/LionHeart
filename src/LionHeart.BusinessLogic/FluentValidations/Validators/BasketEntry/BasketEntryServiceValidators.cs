using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.BasketEntry;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.BasketEntry;

public class BasketEntryServiceValidators
{
    public IBasketEntryValidator BasketEntryValidator { get; }
    public IValidator<AddBasketEntryDto> AddBasketEntryDtoValidator { get; }
    public IValidator<UpdateBasketEntryDto> UpdateBasketEntryDtoValidator { get; }
    public IValidator<IdModel> IdValidator { get; }

    public BasketEntryServiceValidators(IBasketEntryValidator basketEntryValidator,
                                        IValidator<AddBasketEntryDto> addBasketEntryDtoValidator,
                                        IValidator<UpdateBasketEntryDto> updateBasketEntryDtoValidator,
                                        IValidator<IdModel> idValidator)
    {
        BasketEntryValidator = basketEntryValidator;
        AddBasketEntryDtoValidator = addBasketEntryDtoValidator;
        UpdateBasketEntryDtoValidator = updateBasketEntryDtoValidator;
        IdValidator = idValidator;
    }
}