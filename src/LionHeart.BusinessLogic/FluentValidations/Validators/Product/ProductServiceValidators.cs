using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.Product;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Product;

public class ProductServiceValidators
{
    public IProductValidator ProductValidator { get; }
    public IValidator<AddProductDto> AddProductDtoValidator { get; }
    public IValidator<UpdateProductDto> UpdateProductDtoValidator { get; }
    public IValidator<ProductNameModel> ProductNameValidator { get; }
    public IValidator<IdModel> IdValidator { get; }


    public ProductServiceValidators(IProductValidator productValidator,
                                    IValidator<AddProductDto> addProductDtoValidator,
                                    IValidator<UpdateProductDto> updateProductDtoValidator,
                                    IValidator<ProductNameModel> productNameValidator,
                                    IValidator<IdModel> idValidator)
    {
        ProductValidator = productValidator;
        AddProductDtoValidator = addProductDtoValidator;
        UpdateProductDtoValidator = updateProductDtoValidator;
        ProductNameValidator = productNameValidator;
        IdValidator = idValidator;
    }
}