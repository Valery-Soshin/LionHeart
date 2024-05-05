using LionHeart.BusinessLogic.Resources;
using LionHeart.BusinessLogic.FluentValidations.Validators.Brand;
using LionHeart.Core.Dtos.Brand;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Brand;
using LionHeart.BusinessLogic.FluentValidations.Models;

namespace LionHeart.BusinessLogic.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly BrandServiceValidators _validators;

    public BrandService(IBrandRepository brandRepository,
                        BrandServiceValidators validators)
    {
        _brandRepository = brandRepository;
        _validators = validators;
    }

    public async Task<Result<Brand>> GetById(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Brand>.Failure(errorMessages);
            }

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
            var dtoValidationResult = _validators.AddBrandDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Brand>.Failure(errorMessages);
            }

            bool brandAlreadyExists = await _brandRepository.Exists(b => b.Name == dto.Name);
            var validateAddModel = new ValidateAddModel()
            {
                BrandAlreadyExists = brandAlreadyExists
            };
            var brandValidatorResult = _validators.BrandValidator.ValidateAdd(validateAddModel);
            if (brandValidatorResult.IsFaulted)
            {
                return Result<Brand>.Failure(brandValidatorResult.ErrorMessages);
            }

            var brand = new Brand
            {
                Name = dto.Name, 
                ImageId = dto.ImageId
            };
            var brandRepositoryResult = await _brandRepository.Add(brand);
            if (brandRepositoryResult <= 0)
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