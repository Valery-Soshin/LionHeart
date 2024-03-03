using LionHeart.Core.Models;
using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class OrderRepository(ApplicationDbContext dbContext) : RepositoryBase<Order>(dbContext), IOrderRepository
{
    public override Task<Order?> GetById(string id)
	{
		return _dbContext.Orders.AsNoTracking()
            .Include(o => o.Items)
                .ThenInclude(i => i.Details)
            .FirstOrDefaultAsync(o => o.Id == id);
	}
    public Task<List<Order>> GetOrdersByUserId(string userId)
    {
        return _dbContext.Orders.AsNoTracking()
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }
	public override Task<List<Order>> GetAll()
	{
		return _dbContext.Orders.AsNoTracking()
			.Include(o => o.Items)
				.ThenInclude(i => i.Details)
			.ToListAsync();
	}
    public override async Task<int> Update(Order order)
    {
        await EFUpdateHelper.CheckItemsOnDelete(
            order.Items, _dbContext, i => i.OrderId == order.Id);
        await EFUpdateHelper.CheckItemsOnAdd(
            order.Items, _dbContext, i => i.OrderId == order.Id);

        foreach (var item in order.Items)
        {
            await EFUpdateHelper.CheckItemsOnDelete(
                item.Details, _dbContext, d => d.OrderItemId == item.Id);
            await EFUpdateHelper.CheckItemsOnAdd(
                item.Details, _dbContext, d => d.OrderItemId == item.Id);
        }

        return await base.Update(order);
    }
    public Task<bool> Any(string userId)
    {
        return _dbContext.Orders.AsNoTracking()
            .AnyAsync(o => o.UserId == userId);
    }
}