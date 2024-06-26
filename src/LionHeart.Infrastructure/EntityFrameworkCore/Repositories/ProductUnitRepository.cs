﻿using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Repositories;

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
    public Task<List<ProductUnit>> FindProductUnits(List<string> ids)
    {
        return _dbContext.ProductUnits.AsNoTracking()
            .Where(pu => ids.Contains(pu.Id))
            .ToListAsync();
    }
    public Task<int> Count(string productId)
    {
        return _dbContext.ProductUnits.AsNoTracking()
            .CountAsync(pu => pu.ProductId == productId);
    }
}