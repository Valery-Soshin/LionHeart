using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class ProductUnitRepository(ApplicationDbContext dbContext) : RepositoryBase<ProductUnit>(dbContext), IProductUnitRepository
{
    public override Task<ProductUnit?> GetById(string id)
	{
		return _dbContext.ProductUnits.AsNoTracking()
			.FirstOrDefaultAsync(pd => pd.Id == id);
	}
	public Task<List<ProductUnit>> GetByProductId(string productId, int quantity)
	{
		return _dbContext.ProductUnits.AsNoTracking()
			.Where(pu => pu.ProductId == productId)
			.Take(quantity)
			.ToListAsync();
	}
	public override Task<List<ProductUnit>> GetAll()
	{
		return _dbContext.ProductUnits.AsNoTracking()
			.ToListAsync();
	}
	public Task<int> CountByProductId(string productId)
	{
		return _dbContext.ProductUnits.AsNoTracking()
			.CountAsync(pu => pu.ProductId == productId);
	}
}