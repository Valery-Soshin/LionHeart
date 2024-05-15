using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.ProductUnit;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.ProductUnit;

public class ProductUnitServiceValidators
{
    public virtual IProductUnitValidator ProductUnitValidator { get; }
    public virtual IValidator<AddProductUnitDto> AddProductUnitDtoVadidator { get; }
    public virtual IValidator<UpdateProductUnitDto> UpdateProductUnitValidator { get; }
    public virtual IValidator<IdModel> IdValidator { get; }

    public ProductUnitServiceValidators(IProductUnitValidator productUnitValidator,
                                        IValidator<AddProductUnitDto> addProductUnitDtoVadidator,
                                        IValidator<UpdateProductUnitDto> updateProductUnitValidator,
										IValidator<IdModel> idValidator)
	{
        ProductUnitValidator = productUnitValidator;
        AddProductUnitDtoVadidator = addProductUnitDtoVadidator;
        UpdateProductUnitValidator = updateProductUnitValidator;
        IdValidator = idValidator;
    }
}