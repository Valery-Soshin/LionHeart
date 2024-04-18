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
    private readonly IProductRepository _productRepository;
    private readonly IImageService _imageService;

    public ProductService(IProductRepository productRepository,
                          IImageService imageService)
    {
        _productRepository = productRepository;
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
            var product = new Product
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Specifications = dto.Specifications,
                CreatedAt = dto.CreatedAt
            };
            var imageResult = await _imageService.Add(dto.Image);
            if (imageResult.IsFaulted || imageResult.Data is null)
            {
                return new Result<Product>()
                {
                    IsCompleted = false,
                    ErrorMessage = imageResult.ErrorMessage ?? "Image Name is NULL"
                };
            }
            var imageName = imageResult.Data;
            product.Image = new Image()
            {
                File = dto.Image,
                FileName = imageName
            };
            var result = await _productRepository.Add(product);
            if (result <= 0)
            {
                return new Result<Product>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.ProductNotCreated
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
            if (dto.Image is not null && dto.Image.FileName is not null)
            {
                if (product.Image.FileName != dto.Image.FileName)
                {
                    product.Image = new Image
                    {
                        FileName = dto.Image.FileName,
                        File = dto.Image
                    };
                }
            }

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