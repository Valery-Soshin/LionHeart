using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.Brand;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Brand;

public class BrandServiceValidators
{
    public IBrandValidator BrandValidator { get; }
    public IValidator<AddBrandDto> AddBrandDtoValidator { get; }
    public IValidator<IdModel> IdValidator { get; }

    public BrandServiceValidators(IBrandValidator brandValidator,
                                  IValidator<AddBrandDto> addBrandDtoValidator,
                                  IValidator<IdModel> idValidator)
    {
        BrandValidator = brandValidator;
        AddBrandDtoValidator = addBrandDtoValidator;
        IdValidator = idValidator;
    }
}