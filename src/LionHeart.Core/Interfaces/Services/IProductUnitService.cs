using LionHeart.Core.Dtos.ProductUnit;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IProductUnitService
{
    Task<Result<ProductUnit>> GetById(string id);
    Task<Result<ProductUnit>> Add(AddProductUnitDto dto);
    Task<Result<List<ProductUnit>>> AddRange(List<AddProductUnitDto> dtos);
    Task<Result<ProductUnit>> Update(UpdateProductUnitDto dto);
    Task<Result<List<ProductUnit>>> UpdateRange(List<UpdateProductUnitDto> dtos);
    Task<Result<ProductUnit>> Remove(string id);
    Task<Result<int>> Count(string productId);
}