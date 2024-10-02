using System.Diagnostics;
using Serilog;


public class TraceIdMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<TraceIdMiddleware> logger;

    public TraceIdMiddleware(RequestDelegate next, ILogger<TraceIdMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();

        logger.LogInformation("{@TrackId} {@context} ",
            traceId,
            $"Request received with TraceId: {traceId}");
     

        context.Response.Headers.Add("TraceId", traceId);

        await next(context);
    }
}