using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Repositories;

public class OrderItemRepository(ApplicationDbContext dbContext) : RepositoryBase<OrderItem>(dbContext), IOrderItemRepository
{
    public override Task<OrderItem?> GetById(string id)
    {
        return _dbContext.OrderItems.AsNoTracking()
            .SingleOrDefaultAsync(i => i.Id == id);
    }
    public Task<PagedResponse<OrderItem>> GetOrderItemsByFilter(int pageNumber, int pageSize, Expression<Func<OrderItem, bool>> filter)
    {
        var totalRecordsQuery = _dbContext.OrderItems.AsNoTracking();

        var entitiesQuery = _dbContext.OrderItems.AsNoTracking()
            .Include(i => i.Order)
            .Include(i => i.Product);

        return BuildPagination(totalRecordsQuery, entitiesQuery, pageNumber, pageSize, filter);
    }
}