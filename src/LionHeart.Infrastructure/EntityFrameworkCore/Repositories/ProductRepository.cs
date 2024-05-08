using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : RepositoryBase<Product>(dbContext), IProductRepository
{
    public override Task<Product?> GetById(string id)
    {
        return _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Company)
            .Include(p => p.Feedbacks)
            .Include(p => p.Units)
            .Include(p => p.Image)
            .SingleOrDefaultAsync(p => p.Id == id);
    }
    public Task<Product?> GetByName(string name)
    {
        return _dbContext.Products.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Name == name);
    }
    public Task<List<Product>> FindProducts(List<string> ids)
    {
        return _dbContext.Products.AsNoTrackingWithIdentityResolution()
            .Include(p => p.Category)
            .Include(p => p.Units)
            .Include(p => p.Image)
            .Where(p => !p.IsDeleted)
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
    public Task<PagedResponse<Product>> GetProductsByFilter(int pageNumber, int pageSize, Expression<Func<Product, bool>> filter)
    {
        return ExecutePagination(pageNumber, pageSize, filter);
    }
    public Task<PagedResponse<Product>> GetProducts(int pageNumber, int pageSize)
    {
        return ExecutePagination(pageNumber, pageSize);
    }
    public Task<PagedResponse<Product>> Search(string searchedValue, int pageNumber, int pageSize)
    {
        var firstSymbol = searchedValue[0].ToString().ToUpper();
        var lastSymbols = searchedValue[1..].ToLower();
        searchedValue = firstSymbol + lastSymbols;

        Expression<Func<Product, bool>> filter =
            (p) => p.Name == searchedValue ||
                           p.Name.StartsWith(searchedValue);

        return ExecutePagination(pageNumber, pageSize, filter);
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
    private Task<PagedResponse<Product>> ExecutePagination(int pageNumber, int pageSize, Expression<Func<Product, bool>>? filter = null)
    {
        var totalRecordsQuery = _dbContext.Products.AsNoTracking()
            .Where(p => !p.IsDeleted);

        var productsQuery = _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Feedbacks)
            .Include(p => p.Image)
            .Where(p => !p.IsDeleted);

        return BuildPagination(totalRecordsQuery, productsQuery, pageNumber, pageSize, filter);
    }
}