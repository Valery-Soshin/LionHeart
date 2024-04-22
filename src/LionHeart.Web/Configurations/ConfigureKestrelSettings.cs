namespace LionHeart.Web.Configurations;

public static class ConfigureKestrelSettings
{
    public static IWebHostBuilder AddKestrelSettings(this IWebHostBuilder builder)
    {
        builder.UseKestrel(options =>
        {
            options.Limits.MaxRequestBodySize = 1500000; // 1464 kb or 1,43 mb
        });

        return builder;
    }
}