using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.ProductUnit;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class ProductUnitService : IProductUnitService
{
    private readonly IProductUnitRepository _productUnitRepository;

    public ProductUnitService(IProductUnitRepository repository)
    {
        _productUnitRepository = repository;
    }

    public async Task<Result<ProductUnit>> GetById(string id)
    {
        try
        {
            var productUnit = await _productUnitRepository.GetById(id);
            if (productUnit is null) return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotFound);
            
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
            var productUnit = new ProductUnit
            {
                ProductId = dto.ProductId,
                SaleStatus = dto.SaleStatus,
                CreatedAt = dto.CreatedAt
            };
            var result = await _productUnitRepository.Add(productUnit);
            if (result <= 0) return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotCreated);

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
            var productUnits = dtos.Select(d => new ProductUnit
            {
                ProductId = d.ProductId,
                SaleStatus = d.SaleStatus,
                CreatedAt = d.CreatedAt
            }).ToList();
            var result = await _productUnitRepository.AddRange(productUnits);
            if (result <= 0) return Result<List<ProductUnit>>.Failure(ErrorMessage.ProductUnitsNotCreated);

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
            var productUnit = await _productUnitRepository.GetById(dto.Id);
            if (productUnit is null) return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotFound);
            
            productUnit.SaleStatus = dto.SaleStatus;

            var result = await _productUnitRepository.Update(productUnit);
            if (result <= 0) return Result<ProductUnit>.Failure(ErrorMessage.ProductNotUpdated);
            
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
            var productUnits = await _productUnitRepository.FindProductUnits(dtos.Select(d => d.Id).ToList());
            if (productUnits is null) return Result<List<ProductUnit>>.Failure(ErrorMessage.ProductUnitsNotFound);

            foreach (var productUnit in productUnits)
            {
                var productUnitDto = dtos.Single(d => d.Id == productUnit.Id);
                productUnit.SaleStatus = productUnitDto.SaleStatus;
            }

            var result = await _productUnitRepository.UpdateRange(productUnits);
            if (result <= 0) return Result<List<ProductUnit>>.Failure(ErrorMessage.ProductUnitsNotUpdated);

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
            var productUnit = await _productUnitRepository.GetById(id);
            if (productUnit is null) return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotFound);
            
            var result = await _productUnitRepository.Remove(productUnit);
            if (result <= 0) return Result<ProductUnit>.Failure(ErrorMessage.ProductUnitNotRemoved);

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
            var result = await _productUnitRepository.Count(productId);
            return Result<int>.Success(result);
        }
        catch
        {
            return Result<int>.Failure(ErrorMessage.InternalServerError);
        }
    }
}