using LionHeart.BusinessLogic.Helpers;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Product;
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
                return new Result<Product>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductNotFound
                };
            }
            return new Result<Product>
            {
                IsCompleted = true,
                Data = product
            };
        }
        catch
        {
            return new Result<Product>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<Product>>> GetProductsByIds(List<string> ids)
    {
        try
        {
            var products = await _productRepository.GetProductsByIds(ids);
            if (products is null)
            {
                return new Result<List<Product>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotFound
                };
            }
            return new Result<List<Product>>
            {
                IsCompleted = true,
                Data = products
            };
        }
        catch
        {
            return new Result<List<Product>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<Product>>> GetProductsByCategoryId(string categoryId)
    {
        try
        {
            var products = await _productRepository.GetProductsByCategoryId(categoryId);
            if (products is null)
            {
                return new Result<List<Product>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotFound
                };
            }
            return new Result<List<Product>>
            {
                IsCompleted = true,
                Data = products
            };
        }
        catch
        {
            return new Result<List<Product>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<Product>>> GetProductsByUserId(string userId)
    {
        try
        {
            var products = await _productRepository.GetProductsByUserId(userId);
            if (products is null)
            {
                return new Result<List<Product>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotFound
                };
            }
            return new Result<List<Product>>
            {
                IsCompleted = true,
                Data = products
            };
        }
        catch
        {
            return new Result<List<Product>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<PagedResponse<Product>>> GetProductsByCompanyId(string companyId, int pageNumber)
    {
        try
        {
            var pagedResponse = await _productRepository.GetProductsByCompanyId(companyId, pageNumber, PageHelper.PageSize);
            if (pagedResponse is null)
            {
                return new Result<PagedResponse<Product>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotFound
                };
            }
            return new Result<PagedResponse<Product>>
            {
                IsCompleted = true,
                Data = pagedResponse
            };
        }
        catch
        {
            return new Result<PagedResponse<Product>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<PagedResponse<Product>>> GetProducts(int pageNumber)
    {
        try
        {
            var pagedResponse = await _productRepository.GetProducts(pageNumber, PageHelper.PageSize);
            if (pagedResponse is null)
            {
                return new Result<PagedResponse<Product>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotFound
                };
            }
            return new Result<PagedResponse<Product>>
            {
                IsCompleted = true,
                Data = pagedResponse
            };
        }
        catch
        {
            return new Result<PagedResponse<Product>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<PagedResponse<Product>>> Search(string productName, int pageNumber)
    {
        try
        {
            var pagedResponse = await _productRepository.Search(productName, pageNumber, PageHelper.PageSize);
            if (pagedResponse is null)
            {
                return new Result<PagedResponse<Product>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductsNotFound
                };
            }
            return new Result<PagedResponse<Product>>
            {
                IsCompleted = true,
                Data = pagedResponse
            };
        }
        catch
        {
            return new Result<PagedResponse<Product>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Product>> Add(AddProductDto dto)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var imageServiceResult = await _imageService.Add(dto.Image);
            if (imageServiceResult.IsFaulted || imageServiceResult.Data is null)
            {
                return new Result<Product>
                {
                    IsCompleted = false,
                    ErrorMessage = imageServiceResult.ErrorMessage ?? "ImageServiceResult.Data is NULL"
                };
            }
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
                Image = new Image() { FileName = imageServiceResult.Data }
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
                return new Result<Product>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.InternalServerError
                };
            }
            await _unitOfWork.Commit();
            return new Result<Product>
            {
                IsCompleted = true,
                Data = product
            };
        }
        catch
        {
            await _unitOfWork.Rollback();
            return new Result<Product>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Product>> Update(UpdateProductDto dto)
    {
        try
        {
            var product = await _productRepository.GetById(dto.Id);
            if (product is null)
            {
                return new Result<Product>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductNotFound
                };
            }
            product.CategoryId = dto.CategoryId;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.Specifications = dto.Specifications;
            // todo: update image

            var result = await _productRepository.Update(product);
            if (result <= 0)
            {
                return new Result<Product>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductNotUpdated
                };
            }

            return new Result<Product>
            {
                IsCompleted = true,
                Data = product
            };
        }
        catch
        {
            return new Result<Product>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Product>> Remove(string id)
    {
        try
        {
            var product = await _productRepository.GetById(id);
            if (product is null)
            {
                return new Result<Product>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductNotFound
                };
            }
            product.IsDeleted = true;
            await _productRepository.Update(product);
            return new Result<Product>
            {
                IsCompleted = true,
                Data = product
            };
        }
        catch
        {
            return new Result<Product>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}