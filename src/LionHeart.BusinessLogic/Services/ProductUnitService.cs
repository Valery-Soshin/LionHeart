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
            if (productUnit is null)
            {
                return new Result<ProductUnit>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductUnitNotFound
                };
            }
            return new Result<ProductUnit>
            {
                IsCompleted = true,
                Data = productUnit
            };
        }
        catch
        {
            return new Result<ProductUnit>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
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
            if (result <= 0)
            {
                return new Result<ProductUnit>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductUnitNotCreated
                };
            }
            return new Result<ProductUnit>
            {
                IsCompleted = true,
                Data = productUnit
            };
        }
        catch
        {
            return new Result<ProductUnit>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
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
            if (result <= 0)
            {
                return new Result<List<ProductUnit>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductUnitsNotCreated
                };
            }
            return new Result<List<ProductUnit>>
            {
                IsCompleted = true,
                Data = productUnits
            };
        }
        catch
        {
            return new Result<List<ProductUnit>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<ProductUnit>> Remove(RemoveProductUnitDto dto)
    {
        try
        {
            var productUnit = await _productUnitRepository.GetById(dto.Id);
            if (productUnit is null)
            {
                return new Result<ProductUnit>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductUnitNotFound
                };
            }
            var result = await _productUnitRepository.Remove(productUnit);
            if (result <= 0)
            {
                return new Result<ProductUnit>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductUnitNotRemoved
                };
            }
            return new Result<ProductUnit>
            {
                IsCompleted = true,
                Data = productUnit
            };
        }
        catch
        {
            return new Result<ProductUnit>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<int>> Count(string productId)
    {
        try
        {
            return new Result<int>
            {
                IsCompleted = true,
                Data = await _productUnitRepository.Count(productId)
            };
        }
        catch
        {
            return new Result<int>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}