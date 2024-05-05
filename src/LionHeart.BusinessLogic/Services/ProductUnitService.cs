using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.ProductUnit;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.ProductUnit;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.ProductUnit;

namespace LionHeart.BusinessLogic.Services;

public class ProductUnitService : IProductUnitService
{
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly IProductRepository _productRepository;
    private readonly ProductUnitServiceValidators _validators;

    public ProductUnitService(IProductUnitRepository repository,
                              IProductRepository productRepository,
                              ProductUnitServiceValidators validators)
    {
        _productUnitRepository = repository;
        _productRepository = productRepository;
        _validators = validators;
    }

    public async Task<Result<ProductUnit>> GetById(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<ProductUnit>.Failure(errorMessages);
            }

            var productUnit = await _productUnitRepository.GetById(id);
            if (productUnit is null)
            {
                return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotFound);
            }
            return Result<ProductUnit>.Success(productUnit);
        }
        catch
        {
            return Result<ProductUnit>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<ProductUnit>> Add(AddProductUnitDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.AddProductUnitDtoVadidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<ProductUnit>.Failure(errorMessages);
            }

            bool productExists = await _productRepository.Exists(p => p.Id == dto.ProductId);
            var validateAddModel = new ValidateAddModel()
            {
                ProductExists = productExists
            };
            var productUnitValidatorResult = _validators.ProductUnitValidator.ValidateAdd(validateAddModel);
            if (productUnitValidatorResult.IsFaulted)
            {
                return Result<ProductUnit>.Failure(productUnitValidatorResult.ErrorMessages);
            }

            var productUnit = new ProductUnit
            {
                ProductId = dto.ProductId,
                SaleStatus = dto.SaleStatus,
                CreatedAt = dto.CreatedAt
            };
            var productUnitRepositoryResult = await _productUnitRepository.Add(productUnit);
            if (productUnitRepositoryResult <= 0)
            {
                return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotCreated);
            }
            return Result<ProductUnit>.Success(productUnit);
        }
        catch
        {
            return Result<ProductUnit>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<ProductUnit>>> AddRange(List<AddProductUnitDto> dtos)
    {
        try
        {
            foreach (var dto in dtos)
            {
                var dtoValidationResult = _validators.AddProductUnitDtoVadidator.Validate(dto);
                if (!dtoValidationResult.IsValid)
                {
                    var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                    return Result<List<ProductUnit>>.Failure(errorMessages);
                }

                bool productExists = await _productRepository.Exists(p => p.Id == dto.ProductId);
                var validateAddModel = new ValidateAddModel()
                {
                    ProductExists = productExists
                };
                var productUnitValidatorResult = _validators.ProductUnitValidator.ValidateAdd(validateAddModel);
                if (productUnitValidatorResult.IsFaulted)
                {
                    return Result<List<ProductUnit>>.Failure(productUnitValidatorResult.ErrorMessages);
                }
            }

            var productUnits = dtos.Select(d => new ProductUnit
            {
                ProductId = d.ProductId,
                SaleStatus = d.SaleStatus,
                CreatedAt = d.CreatedAt
            }).ToList();
            var productUnitRepositoryResult = await _productUnitRepository.AddRange(productUnits);
            if (productUnitRepositoryResult <= 0)
            {
                return Result<List<ProductUnit>>.Failure(ErrorMessage.ProductUnitsNotCreated);
            }
            return Result<List<ProductUnit>>.Success(productUnits);
        }
        catch
        {
            return Result<List<ProductUnit>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<ProductUnit>> Update(UpdateProductUnitDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.UpdateProductUnitValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<ProductUnit>.Failure(errorMessages);
            }

            bool productUnitExists = await _productUnitRepository.Exists(u => u.Id == dto.Id);
            var validateUpdateModel = new ValidateUpdateModel()
            {
                ProductUnitExists = productUnitExists
            };
            var productUnitValidatorResult = _validators.ProductUnitValidator.ValidateUpdate(validateUpdateModel);
            if (productUnitValidatorResult.IsFaulted)
            {
                return Result<ProductUnit>.Failure(productUnitValidatorResult.ErrorMessages);
            }

            var productUnit = await _productUnitRepository.GetById(dto.Id);
            if (productUnit is null)
            {
                return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotFound);
            }
            productUnit.SaleStatus = dto.SaleStatus;
            var productUnitRepositoryResult = await _productUnitRepository.Update(productUnit);
            if (productUnitRepositoryResult <= 0)
            {
                return Result<ProductUnit>.Failure(ErrorMessage.ProductNotUpdated);
            }
            return Result<ProductUnit>.Success(productUnit);
        }
        catch
        {
            return Result<ProductUnit>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<ProductUnit>>> UpdateRange(List<UpdateProductUnitDto> dtos)
    {
        try
        {
            foreach (var dto in dtos)
            {
                var dtoValidationResult = _validators.UpdateProductUnitValidator.Validate(dto);
                if (!dtoValidationResult.IsValid)
                {
                    var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                    return Result<List<ProductUnit>>.Failure(errorMessages);
                }

                bool productUnitExists = await _productUnitRepository.Exists(u => u.Id == dto.Id);
                var validateUpdateModel = new ValidateUpdateModel()
                {
                    ProductUnitExists = productUnitExists
                };
                var productUnitValidatorResult = _validators.ProductUnitValidator.ValidateUpdate(validateUpdateModel);
                if (productUnitValidatorResult.IsFaulted)
                {
                    return Result<List<ProductUnit>>.Failure(productUnitValidatorResult.ErrorMessages);
                }
            }

            var productUnits = await _productUnitRepository.FindProductUnits(dtos.Select(d => d.Id).ToList());
            if (productUnits is null)
            {
                return Result<List<ProductUnit>>.Failure(ErrorMessage.ProductUnitsNotFound);
            }
            foreach (var productUnit in productUnits)
            {
                var productUnitDto = dtos.Single(d => d.Id == productUnit.Id);
                productUnit.SaleStatus = productUnitDto.SaleStatus;
            }
            var productUnitRepositoryResult = await _productUnitRepository.UpdateRange(productUnits);
            if (productUnitRepositoryResult <= 0)
            {
                return Result<List<ProductUnit>>.Failure(ErrorMessage.ProductUnitsNotUpdated);
            }
            return Result<List<ProductUnit>>.Success(productUnits);
        }
        catch
        {
            return Result<List<ProductUnit>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<ProductUnit>> Remove(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<ProductUnit>.Failure(errorMessages);
            }

            var productUnit = await _productUnitRepository.GetById(id);
            if (productUnit is null)
            {
                return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotFound);
            }
            var productUnitRepositoryResult = await _productUnitRepository.Remove(productUnit);
            if (productUnitRepositoryResult <= 0)
            {
                return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotRemoved);
            }
            return Result<ProductUnit>.Success(productUnit);
        }
        catch
        {
            return Result<ProductUnit>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<int>> Count(string productId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(productId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<int>.Failure(errorMessages);
            }

            int productUnitCount = await _productUnitRepository.Count(productId);
            return Result<int>.Success(productUnitCount);
        }
        catch
        {
            return Result<int>.Failure(ErrorMessage.InternalServerError);
        }
    }
}