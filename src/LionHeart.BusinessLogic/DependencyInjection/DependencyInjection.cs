using FluentValidation;
using LionHeart.BusinessLogic.CoreValidations;
using LionHeart.BusinessLogic.Services;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.BasketEntry;
using LionHeart.BusinessLogic.FluentValidations.Validators.Brand;
using LionHeart.BusinessLogic.FluentValidations.Validators.Product;
using LionHeart.BusinessLogic.FluentValidations.Validators.Product.Property;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.BasketEntry;
using LionHeart.Core.Dtos.Brand;
using LionHeart.Core.Dtos.Product;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Interfaces.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LionHeart.Core.Dtos.Company;
using LionHeart.BusinessLogic.FluentValidations.Validators.Company;
using LionHeart.Core.Dtos.Feedback;
using LionHeart.BusinessLogic.FluentValidations.Validators.Feedback;
using LionHeart.BusinessLogic.FluentValidations.Validators.FavoriteProduct;
using LionHeart.Core.Dtos.Category;
using LionHeart.BusinessLogic.FluentValidations.Validators.Category;
using LionHeart.Core.Dtos.Notification;
using LionHeart.BusinessLogic.FluentValidations.Validators.Notification;
using LionHeart.Core.Dtos.Order;
using LionHeart.BusinessLogic.FluentValidations.Validators.Order;
using LionHeart.Core.Dtos.ProductUnit;
using LionHeart.BusinessLogic.FluentValidations.Validators.ProductUnit;

namespace LionHeart.BusinessLogic.DependencyInjection;

public static class DependencyInjection
{
    public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        services.ServicesInit();
        services.ValidationsInit();
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
    private static void ValidationsInit(this IServiceCollection services)
    {
        services.AddScoped<IValidator<IdModel>, IdValidator>();
        services.AddScoped<IValidator<DateTimeOffsetModel>, DateTimeOffsetValidator>();

        services.AddScoped<IValidator<AddBasketEntryDto>, AddBasketEntryDtoValidator>();
        services.AddScoped<IValidator<UpdateBasketEntryDto>, UpdateBasketEntryDtoValidator>();
        services.AddScoped<IBasketEntryValidator, BasketEntryValidator>();
        services.AddScoped<BasketEntryServiceValidators>();

        services.AddScoped<IValidator<AddBrandDto>, AddBrandDtoValidator>();
        services.AddScoped<IBrandValidator, BrandValidator>();
        services.AddScoped<BrandServiceValidators>();

        services.AddScoped<IValidator<AddCategoryDto>, AddCategoryDtoValidator>();
        services.AddScoped<ICategoryValidator, CategoryValidator>();
        services.AddScoped<CategoryServiceValidators>();

        services.AddScoped<IValidator<AddCompanyDto>, AddCompanyDtoValidator>();
        services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryDtoValidator>();
        services.AddScoped<ICompanyValidator, CompanyValidator>();
        services.AddScoped<CompanyServiceValidators>();

        services.AddScoped<IFavoriteProductValidator, FavoriteProductValidator>();
        services.AddScoped<FavoriteProductServiceValidators>();

        services.AddScoped<IValidator<AddFeedbackDto>, AddFeedbackDtoValidator>();
        services.AddScoped<IFeedbackValidator, FeedbackValidator>();
        services.AddScoped<FeedbackServiceValidators>();

        services.AddScoped<IValidator<AddNotificationDto>, AddNotificationDtoValidator>();
        services.AddScoped<INotificationValidator, NotificationValidator>();
        services.AddScoped<NotificationServiceValidators>();

        services.AddScoped<IValidator<AddOrderDto>, AddOrderDtoValidator>();
        services.AddScoped<IValidator<AddOrderProductDto>, AddOrderProductDtoValidator>();
        services.AddScoped<IOrderValidator, OrderValidator>();
        services.AddScoped<OrderServiceValidators>();

        services.AddScoped<IValidator<AddProductDto>, AddProductDtoValidator>();
        services.AddScoped<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();
        services.AddScoped<IValidator<ProductNameModel>, ProductNameValidator>();
        services.AddScoped<IProductValidator, ProductValidator>();
        services.AddScoped<ProductServiceValidators>();

        services.AddScoped<IValidator<AddProductUnitDto>, AddProductUnitValidator>();
        services.AddScoped<IValidator<UpdateProductUnitDto>, UpdateProductUnitDtoValidator>();
        services.AddScoped<IProductUnitValidator, ProductUnitValidator>();
        services.AddScoped<ProductUnitServiceValidators>();
    }
}