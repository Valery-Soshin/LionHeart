using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Brand;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<Result<Brand>> GetById(string id)
    {
        try
        {
            var brand = await _brandRepository.GetById(id);
            if (brand is null)
            {
                return Result<Brand>.Failure(ErrorMessage.BrandNotFound);
            }
            return Result<Brand>.Success(brand);
        }
        catch
        {
            return Result<Brand>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<Brand>>> GetBrands()
    {
        try
        {
            var brands = await _brandRepository.GetBrands();
            if (brands.Count == 0)
            {
                return Result<List<Brand>>.Failure(ErrorMessage.BrandsNotFound);
            }
            return Result<List<Brand>>.Success(brands);
        }
        catch
        {
            return Result<List<Brand>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Brand>> Add(AddBrandDto dto)
    {
        try
        {
            var brand = new Brand
            {
                Name = dto.Name, 
                ImageId = dto.ImageId
            };
            var result = await _brandRepository.Add(brand);
            if (result <= 0)
            {
                return Result<Brand>.Failure(ErrorMessage.BrandNotCreated);
            }
            return Result<Brand>.Success(brand);
        }
        catch
        {
            return Result<Brand>.Failure(ErrorMessage.InternalServerError);
        }
    }
} 