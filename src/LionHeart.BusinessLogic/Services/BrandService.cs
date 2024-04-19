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
                return new Result<Brand>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BrandNotFound
                };
            }
            return new Result<Brand>
            {
                IsCompleted = true,
                Data = brand
            };
        }
        catch
        {
            return new Result<Brand>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
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
                return new Result<Brand>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.BrandNotCreated
                };
            }
            return new Result<Brand>
            {
                IsCompleted = true,
                Data = brand
            };
        }
        catch
        {
            return new Result<Brand>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
} 