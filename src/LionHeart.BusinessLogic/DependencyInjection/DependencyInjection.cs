using LionHeart.BusinessLogic.Services;
using LionHeart.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LionHeart.BusinessLogic.DependencyInjection;

public static class DependencyInjection
{
    public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        services.ServicesInit();
    }

    private static void ServicesInit(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IProductUnitService, ProductUnitService>();
        services.AddScoped<IBasketEntryService, BasketEntryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IFavoriteProductService, FavoriteProductService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IBrandService, BrandService>();
    }
}
