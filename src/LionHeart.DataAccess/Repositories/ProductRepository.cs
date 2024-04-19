using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionHeart.DataAccess.Repositories;

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
            .Where(p => p.Company.UserId == userId)
            .ToListAsync();
    }
    public Task<PagedResponse<Product>> GetProductsByCompanyId(string companyId, int pageNumber, int pageSize)
    {
        Expression<Func<Product, bool>> filter = (Product p) => p.CompanyId == companyId;
        return ExecutePagination(pageNumber, pageSize, filter);
    }
    public Task<PagedResponse<Product>> GetProductsByBrandId(string brandId, int pageNumber, int pageSize)
    {
        Expression<Func<Product, bool>> filter = (Product p) => p.BrandId == brandId;
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
            (Product p) => p.Name == searchedValue ||
                           p.Name.StartsWith(searchedValue) ||
                           p.Category.Name == searchedValue ||
                           p.Category.Name.StartsWith(searchedValue) ||
                           p.Company.Name == searchedValue ||
                           p.Company.Name.StartsWith(searchedValue);

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

    private async Task<PagedResponse<Product>> ExecutePagination(int pageNumber, int pageSize, Expression<Func<Product, bool>>? filter = null)
    {
        var totalRecordsQuery = _dbContext.Products.AsNoTracking()
            .Where(p => !p.IsDeleted);

        var productsQuery = _dbContext.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Feedbacks)
            .Include(p => p.Image)
            .Where(p => !p.IsDeleted);

        if (filter is not null)
        {
            totalRecordsQuery = totalRecordsQuery.Where(filter);
            productsQuery = productsQuery.Where(filter);
        }

        var totalRecords = await totalRecordsQuery
            .CountAsync();

        var products = await productsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<Product>(products, totalRecords, pageNumber, pageSize);
    }
}