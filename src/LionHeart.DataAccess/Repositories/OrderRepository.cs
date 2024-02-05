using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
	public OrderRepository(ApplicationDbContext dbContext)
	: base(dbContext) { }

	public override Task<Order?> GetById(string id)
	{
		return _dbContext.Orders.AsNoTracking()
			.Include(o => o.User)
			.Include(o => o.Product)
			.FirstOrDefaultAsync(o => o.Id == id);
	}
	public override Task<List<Order>> GetAll()
	{
		return _dbContext.Orders.AsNoTracking()
			.Include(o => o.User)
			.Include(o => o.Product)
			.ToListAsync();
	}
}