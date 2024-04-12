using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : RepositoryBase<Product>(dbContext), IProductRepository
{
    public override Task<Product?> GetById(string id)
    {
        return _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Feedbacks)
                .ThenInclude(f => f.User)
            .Include(p => p.Units)
            .Include(p => p.Image)
            .SingleOrDefaultAsync(p => p.Id == id);
    }
    public Task<List<Product>> GetProductsByIds(List<string> ids)
    {
        return _dbContext.Products.AsNoTrackingWithIdentityResolution()
            .Include(p => p.Category)
            .Include(p => p.Units)
            .Include(p => p.Image)
            .Where(p => !p.IsDeleted)
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
    public Task<List<Product>> GetProductsByCategoryId(string categoryId)
    {
        return _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Image)
            .Where(p => !p.IsDeleted)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
    public Task<List<Product>> GetProductsByUserId(string userId)
    {
        return _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Image)
            .Where(p => !p.IsDeleted)
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }
    public async Task<PagedResponse> GetProductsWithPagination(int pageNumber, int pageSize)
    {
        var totalRecords = await _dbContext.Products.AsNoTracking()
            .Where(p => !p.IsDeleted)
            .CountAsync();

        var products = await _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Image)
            .Where(p => !p.IsDeleted)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse(products, totalRecords, pageNumber, pageSize);
    }
    public Task<List<Product>> Search(string productName)
    {
        var firstSymbol = productName[0].ToString().ToUpper();
        var lastSymbols = productName[1..].ToLower();

        productName = firstSymbol + lastSymbols;

        return _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Image)
            .Where(p => !p.IsDeleted)
            .Where(p => p.Name == productName ||
                        p.Name.StartsWith(productName))
            .ToListAsync();
    }
    public override async Task<int> Update(Product product)
    {
        await EFUpdateHelper.CheckItemsOnDelete(
            product.Units, _dbContext, u => u.ProductId == product.Id);

        await EFUpdateHelper.CheckItemsOnAdd(
            product.Units, _dbContext, u => u.ProductId == product.Id);

        return await base.Update(product);
    }
    public override async Task<int> UpdateRange(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            await EFUpdateHelper.CheckItemsOnDelete(
            product.Units, _dbContext, u => u.ProductId == product.Id);

            await EFUpdateHelper.CheckItemsOnAdd(
                product.Units, _dbContext, u => u.ProductId == product.Id);
        }

        return await base.UpdateRange(products);
    }
}