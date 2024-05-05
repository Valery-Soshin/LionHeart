using LionHeart.BusinessLogic.Helpers;
using LionHeart.BusinessLogic.Resources;
using LionHeart.BusinessLogic.FluentValidations.Validators.Product;
using LionHeart.Core.Dtos.Product;
using LionHeart.Core.Enums;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Product;
using LionHeart.BusinessLogic.FluentValidations.Models;

namespace LionHeart.BusinessLogic.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ProductServiceValidators _validators;
    private readonly IImageService _imageService;

    public ProductService(IUnitOfWork unitOfWork,
                          IProductRepository productRepository,
                          IProductUnitRepository productUnitRepository,
                          ICategoryRepository categoryRepository,
                          IBrandRepository brandRepository,
                          ICompanyRepository companyRepository,
                          ProductServiceValidators validators,
                          IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _categoryRepository = categoryRepository;
        _brandRepository = brandRepository;
        _companyRepository = companyRepository;
        _validators = validators;
        _imageService = imageService;
    }

    public async Task<Result<Product>> GetById(string id)
    {
        try
        {
            var idValidationResult =_validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Product>.Failure(errorMessages);
            }

            var product = await _productRepository.GetById(id);
            if (product is null)
            {
                return Result<Product>.Failure(ErrorMessage.ProductNotFound);
            }
            return Result<Product>.Success(product);
        }
        catch
        {
            return Result<Product>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Product>>> GetProductsByCategoryId(string categoryId, int pageNumber)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(categoryId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<Product>>.Failure(errorMessages);
            }

            var page = await _productRepository.GetProductsByFilter(
                 pageNumber, PageHelper.PageSize, p => p.CategoryId == categoryId);

            return Result<PagedResponse<Product>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Product>>> GetProductsByUserId(string userId, int pageNumber)
    {
        try
        {
            var idValidationResult =  _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<Product>>.Failure(errorMessages);
            }

            var page = await _productRepository.GetProductsByFilter(
                 pageNumber, PageHelper.PageSize, p => p.Company.UserId == userId);

            return Result<PagedResponse<Product>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Product>>> GetProductsByCompanyId(string companyId, int pageNumber)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(companyId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<Product>>.Failure(errorMessages);
            }

            var page = await _productRepository.GetProductsByFilter(
                pageNumber, PageHelper.PageSize, p => p.CompanyId == companyId);

            return Result<PagedResponse<Product>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Product>>> GetProductsByBrandId(string brandId, int pageNumber)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(brandId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<Product>>.Failure(errorMessages);
            }

            var page = await _productRepository.GetProductsByFilter(
                 pageNumber, PageHelper.PageSize, p => p.BrandId == brandId);

            return Result<PagedResponse<Product>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Product>>> GetAll(int pageNumber)
    {
        try
        {
            var page = await _productRepository.GetProducts(pageNumber, PageHelper.PageSize);
            return Result<PagedResponse<Product>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Product>>> Search(string productName, int pageNumber)
    {
        try
        {
            var productNameValidationResult = _validators.ProductNameValidator
                .Validate(new ProductNameModel(productName));

            if (!productNameValidationResult.IsValid)
            {
                var errorMessages = productNameValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<Product>>.Failure(errorMessages);
            }

            var page = await _productRepository.Search(productName, pageNumber, PageHelper.PageSize);
            return Result<PagedResponse<Product>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<Product>>> FindProducts(List<string> ids)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(ids));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<List<Product>>.Failure(errorMessages);
            }

            var products = await _productRepository.FindProducts(ids);
            return Result<List<Product>>.Success(products);
        }
        catch
        {
            return Result<List<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Product>> Add(AddProductDto dto)
    {
        try
        {
            var dtoValidatorResult = _validators.AddProductDtoValidator.Validate(dto);
            if (!dtoValidatorResult.IsValid)
            {
                var errorMessages = dtoValidatorResult.Errors.Select(e => e.ErrorMessage);
                return Result<Product>.Failure(errorMessages);
            }

            bool productAlreadyExists = await _productRepository.Exists(p => p.Name == dto.Name);
            bool categoryExists = await _categoryRepository.Exists(c => c.Id == dto.CategoryId);
            bool brandExists = await _brandRepository.Exists(b => b.Id == dto.BrandId);
            bool companyExists = await _companyRepository.Exists(c => c.Id == dto.CompanyId);
            var validateAddModel = new ValidateAddModel
            {
                ProductAlreadyExists = productAlreadyExists,
                CategoryExists = categoryExists,
                BrandExists = brandExists,
                CompanyExists = companyExists
            };
            var productValidatorResult = _validators.ProductValidator.ValidateAdd(validateAddModel);
            if (productValidatorResult.IsFaulted)
            {
                return Result<Product>.Failure(productValidatorResult.ErrorMessages);
            }

            await _unitOfWork.BeginTransaction();

            var imageServiceResult = await _imageService.Add(dto.Image);
            if (imageServiceResult.IsFaulted)
            {
                await _unitOfWork.Rollback();
                return Result<Product>.Failure(imageServiceResult.ErrorMessages);
            }
            var imageName = imageServiceResult.Value;

            var product = new Product
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                BrandId = dto.BrandId,
                CompanyId = dto.CompanyId,
                Price = dto.Price,
                Description = dto.Description,
                Specifications = dto.Specifications,
                CreatedAt = dto.CreatedAt,
                Image = new Image(imageName)
            };
            var productRepositoryResult = await _productRepository.Add(product);
            if (productRepositoryResult <= 0)
            {
                await _unitOfWork.Rollback();
                return Result<Product>.Failure(ErrorMessage.ProductNotCreated);
            }

            var productUnits = Enumerable.Range(0, dto.Quantity).Select(i => new ProductUnit
            {
                ProductId = product.Id,
                CreatedAt = product.CreatedAt,
                SaleStatus = SaleStatus.Available
            }).ToList();
            var productUnitRepositoryResult = await _productUnitRepository.AddRange(productUnits);
            if (productUnitRepositoryResult <= 0)
            {
                await _unitOfWork.Rollback();
                return Result<Product>.Failure(ErrorMessage.ProductUnitsNotCreated);
            }

            await _unitOfWork.Commit();
            return Result<Product>.Success(product);
        }
        catch
        {
            if (_unitOfWork.IsTransactionActive)
            {
                await _unitOfWork.Rollback();
            }
            return Result<Product>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Product>> Update(UpdateProductDto dto)
    {
        try
        {
            var dtolValidationResult = _validators.UpdateProductDtoValidator.Validate(dto);
            if (!dtolValidationResult.IsValid)
            {
                var errorMessages = dtolValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Product>.Failure(errorMessages);
            }

            bool productExists = await _productRepository.Exists(p => p.Id == dto.Id);
            bool categoryExists = await _categoryRepository.Exists(c => c.Id == dto.CategoryId);
            bool brandExists = await _brandRepository.Exists(b => b.Id == dto.BrandId);
            var validateUpdateModel = new ValidateUpdateModel
            {
                ProductExists = productExists,
                CategoryExists = categoryExists,
                BrandExists = brandExists
            };
            var productValidatorResult = _validators.ProductValidator.ValidateUpdate(validateUpdateModel);
            if (productValidatorResult.IsFaulted)
            {
                return Result<Product>.Failure(productValidatorResult.ErrorMessages);
            }

            var product = await _productRepository.GetById(dto.Id);
            if (product is null) return Result<Product>.Failure(ErrorMessage.ProductNotFound);

            product.CategoryId = dto.CategoryId;
            product.BrandId = dto.BrandId;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.Specifications = dto.Specifications;
            // todo: update image

            var productRepositoryResult = await _productRepository.Update(product);
            if (productRepositoryResult <= 0)
            {
                return Result<Product>.Failure(ErrorMessage.ProductNotUpdated);
            }
            return Result<Product>.Success(product);
        }
        catch
        {
            return Result<Product>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Product>> Remove(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Product>.Failure(errorMessages);
            }

            bool productExists = await _productRepository.Exists(p => p.Id == id);
            var validateRemoveModel = new ValidateRemoveModel(productExists);
            var productValidatorResult = _validators.ProductValidator.ValidateRemove(validateRemoveModel);
            if (productValidatorResult.IsFaulted)
            {
                return Result<Product>.Failure(productValidatorResult.ErrorMessages);
            }

            var product = await _productRepository.GetById(id);
            if (product is null)
            {
                return Result<Product>.Failure(ErrorMessage.ProductNotFound);
            }
            product.IsDeleted = true;
            var productRepositoryResult = await _productRepository.Update(product);
            if (productRepositoryResult <= 0)
            {
                return Result<Product>.Failure(ErrorMessage.ProductNotRemoved);
            }
            return Result<Product>.Success(product);
        }
        catch
        {
            return Result<Product>.Failure(ErrorMessage.InternalServerError);
        }
    }
}