using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class ProductDetailRepository : RepositoryBase<ProductDetail>, IProductDetailRepository
{
	public ProductDetailRepository(ApplicationDbContext dbContext)
		: base(dbContext) { }

	public override Task<ProductDetail?> GetById(string id)
	{
		return _dbContext.ProductDetails.AsNoTracking()
			.FirstOrDefaultAsync(pd => pd.Id == id);
	}
	public Task<List<ProductDetail>> GetByProductId(string productId, int quantity)
	{
		return _dbContext.ProductDetails.AsNoTracking()
			.Where(pd => pd.ProductId == productId)
			.Take(quantity)
			.ToListAsync();
	}
	public override Task<List<ProductDetail>> GetAll()
	{
		return _dbContext.ProductDetails.AsNoTracking()
			.ToListAsync();
	}
	public Task<int> CountByProductId(string productId)
	{
		return _dbContext.ProductDetails.AsNoTracking()
			.CountAsync(pd => pd.ProductId == productId);
	}
}