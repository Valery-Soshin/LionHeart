using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using LionHeart.Core.Services;

namespace LionHeart.BusinessLogic.Services;

public class ProductDetailService : IProductDetailService
{
	private readonly IProductDetailRepository _productDetailRepository;

    public ProductDetailService(IProductDetailRepository productDetailRepository)
    {
		_productDetailRepository = productDetailRepository;
    }

    public Task<ProductDetail?> GetById(string id)
	{
		return _productDetailRepository.GetById(id);
	}
	public Task<List<ProductDetail>> GetByProductId(string productId, int quantity)
	{
		return _productDetailRepository.GetByProductId(productId, quantity);
	}
	public Task<List<ProductDetail>> GetAll()
	{
		return _productDetailRepository.GetAll();
	}
	public Task<int> Add(ProductDetail product)
	{
		return _productDetailRepository.Add(product);
	}
	public Task<int> Update(ProductDetail product)
	{
		return _productDetailRepository.Update(product);
	}
	public Task<int> Remove(ProductDetail product)
	{
		return _productDetailRepository.Remove(product);
	}
	public Task<int> CountByProductId(string productId)
	{
		return _productDetailRepository.CountByProductId(productId);
	}
}