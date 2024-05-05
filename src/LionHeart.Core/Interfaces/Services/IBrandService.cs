using LionHeart.Core.Dtos.Brand;
using LionHeart.Core.Models;
using LionHeart.Core.Results;

namespace LionHeart.Core.Interfaces.Services;

public interface IBrandService
{
    Task<Result<Brand>> GetById(string id);
    Task<Result<List<Brand>>> GetBrands();
    Task<Result<Brand>> Add(AddBrandDto dto);
}