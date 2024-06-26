﻿using LionHeart.Web.Middeware;

namespace LionHeart.Web.Helpers;

public static class ExtensionsHelper
{
    public static IApplicationBuilder UseLoggingRequestTimeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingRequestTimeMiddleware>();
    }
}