using NLog;
using Serilog;

namespace LoggingBestPract.Api.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public LoggingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        Log.Information("Request received from IP Address: {IpAddress}", ipAddress);
        logger.Info("Request received from IP Address: {IpAddress}", ipAddress);
        await next(context);
    }
}