using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.Graylog;
using LoggingBestPract.Application.Logging;

namespace LoggingBestPract.Application.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseCustomSerilog(this IHostBuilder builder, IConfiguration configuration, string applicationName)
    {
        var graylogSinkApplicationOptions =
            configuration.GetSection(nameof(GraylogSinkApplicationOptions)).Get<GraylogSinkApplicationOptions>() ??
            throw new ArgumentNullException(nameof(GraylogSinkApplicationOptions));
        builder.ConfigureLogging((context, loggingBuilder) =>
        {
            loggingBuilder.ClearProviders();
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(context.Configuration)
                .MinimumLevel.Information()
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
                .MinimumLevel.Override("Hangfire", LogEventLevel.Warning)
                .MinimumLevel.Override("Default", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .WriteTo.Async(s => s.Console(new JsonFormatter()))
                .WriteTo.Graylog(new GraylogSinkOptions
                {
                    HostnameOrAddress = graylogSinkApplicationOptions.HostnameOrAddress,
                    Port = graylogSinkApplicationOptions.Port,
                    Facility = applicationName,
                })
                .CreateLogger();
            loggingBuilder.Services.AddSingleton(_ => (ILoggerFactory)new Serilog.Extensions.Logging.SerilogLoggerFactory(logger, true));
        });
        return builder;
    }
}