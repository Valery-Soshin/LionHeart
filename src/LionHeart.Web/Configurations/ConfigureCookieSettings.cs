namespace LionHeart.Web.Configurations;

public static class ConfigureCookieSettings
{
    public static IServiceCollection AddCookieSettings(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
            options.AccessDeniedPath = "/Home/Error";
        });

        return services;
    }
}