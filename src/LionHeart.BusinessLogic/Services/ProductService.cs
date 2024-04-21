using LionHeart.BusinessLogic.Helpers;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Product;
using LionHeart.Core.Dtos.ProductUnit;
using LionHeart.Core.Enums;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitService _productUnitService;
    private readonly IImageService _imageService;

    public ProductService(IUnitOfWork unitOfWork,
                          IProductRepository productRepository,
                          IProductUnitService productUnitService,
                          IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productUnitService = productUnitService;
        _imageService = imageService;
    }

    public async Task<Result<Product>> GetById(string id)
    {
        try
        {
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
    public async Task<Result<List<Product>>> FindProducts(List<string> ids)
    {
        try
        {
            var products = await _productRepository.FindProducts(ids);
            if (products is null)
            {
                return Result<List<Product>>.Failure(ErrorMessage.ProductsNotFound);
            }
            return Result<List<Product>>.Success(products);
        }
        catch
        {
            return Result<List<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Product>>> GetProductsByCategoryId(string categoryId, int pageNumber)
    {
        try
        {
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
            var page = await _productRepository.Search(productName, pageNumber, PageHelper.PageSize);
            return Result<PagedResponse<Product>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Product>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Product>> Add(AddProductDto dto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var imageServiceResult = await _imageService.Add(dto.Image);
            if (imageServiceResult.IsFaulted) return Result<Product>.Failure(imageServiceResult.ErrorMessages);

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
                Image = new Image() { FileName = imageServiceResult.Value }
            };
            var productRepositoryResult = await _productRepository.Add(product);

            var productUnitDtos = Enumerable.Range(0, dto.Quantity).Select(i => new AddProductUnitDto
            {
                ProductId = product.Id,
                CreatedAt = product.CreatedAt,
                SaleStatus = SaleStatus.Available
            }).ToList();
            var productUnitServiceResult = await _productUnitService.AddRange(productUnitDtos);

            if (productUnitServiceResult.IsFaulted ||
                productRepositoryResult <= 0)
            {
                await _unitOfWork.Rollback();
                return Result<Product>.Failure(ErrorMessage.InternalServerError);
            }
            await _unitOfWork.Commit();
            return Result<Product>.Success(product);
        }
        catch
        {
            await _unitOfWork.Rollback();
            return Result<Product>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Product>> Update(UpdateProductDto dto)
    {
        try
        {
            var product = await _productRepository.GetById(dto.Id);
            if (product is null)
            {
                return Result<Product>.Failure(ErrorMessage.ProductNotFound);
            }
            product.CategoryId = dto.CategoryId;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.Specifications = dto.Specifications;
            // todo: update image

            var result = await _productRepository.Update(product);
            if (result <= 0) return Result<Product>.Failure(ErrorMessage.ProductNotUpdated);

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
            var product = await _productRepository.GetById(id);
            if (product is null)
            {
                var errorMessages = new List<string>
                {
                    ErrorMessage.ProductNotFound,
                    ErrorMessage.ProductNotRemoved
                };
                return Result<Product>.Failure(errorMessages);
            }
            product.IsDeleted = true;

            var result = await _productRepository.Update(product);
            if (result <= 0) return Result<Product>.Failure(ErrorMessage.ProductNotRemoved);

            return Result<Product>.Success(product);
        }
        catch
        {
            return Result<Product>.Failure(ErrorMessage.InternalServerError);
        }
    }
}