using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
    public Task<PagedResponse<Order>> GetOrdersByFilter(int pageNumber, int pageSize, Expression<Func<Order, bool>> filter)
    {
        return ExecutePagination(pageNumber, pageSize, filter);
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
    public Task<bool> Exists(string userId, string productId)
    {
        return _dbContext.Orders.AsNoTracking()
            .AnyAsync(o => o.UserId == userId &&
                           o.Items.Any(i => i.ProductId == productId));
    }

    private Task<PagedResponse<Order>> ExecutePagination(int pageNumber, int pageSize, Expression<Func<Order, bool>> filter)
    {
        var totalRecordsQuery = _dbContext.Orders.AsNoTracking();

        var ordersQuery = _dbContext.Orders.AsNoTracking()
            .Include(o => o.Items).ThenInclude(i => i.Product);

        return BuildPagination(totalRecordsQuery, ordersQuery, pageNumber, pageSize, filter);
    }
}