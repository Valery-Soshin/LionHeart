using LionHeart.Core.Interfaces.Repositories;
using LionHeart.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LionHeart.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace LionHeart.DataAccess.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(configuration["ConnectionStrings:PostgreSql"]));

        services.RepositoriesInit();
    }

    private static void RepositoriesInit(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IProductUnitRepository, ProductUnitRepository>();
        services.AddScoped<IBasketEntryRepository, BasketEntryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
    }
}