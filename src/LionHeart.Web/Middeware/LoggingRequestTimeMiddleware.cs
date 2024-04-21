using System.Diagnostics;

namespace LionHeart.Web.Middeware;

public class LoggingRequestTimeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingRequestTimeMiddleware> _logger;

    public LoggingRequestTimeMiddleware(RequestDelegate next,
                                        ILogger<LoggingRequestTimeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var timer = Stopwatch.StartNew();
        await _next.Invoke(context);
        timer.Stop();
        _logger.LogInformation($"{context.Request.Path}, time: {timer.ElapsedMilliseconds / 1000.0} сек");
    }
}