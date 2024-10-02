// using System.Diagnostics;
// using Microsoft.AspNetCore.HttpLogging;
// using Microsoft.AspNetCore.Mvc;
// using LoggingBestPract.Application.Exceptions;
// using Microsoft.OpenApi.Models;
// using Serilog;
// using Serilog.Events;
// using LoggingBestPract.Api.Middlewares;
// using LoggingBestPract.Application.Extensions;
// using LoggingBestPract.Persistence.Extensions;
// using LoggingBestPract.WebAPI.Extensions;
// //
//
// var builder = WebApplication.CreateBuilder(args);
//
// var configurationBuilderBuilder = new ConfigurationBuilder();
// configurationBuilderBuilder
//     .AddJsonFile("appsettings.json")
//     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
//
// Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", builder.Environment.EnvironmentName);
// configurationBuilderBuilder.AddEnvironmentVariables();
//
// IConfigurationRoot configuration = configurationBuilderBuilder.Build();
//
// builder.Services.AddControllers();
// builder.Services.Configure<ApiBehaviorOptions>(o => { o.InvalidModelStateResponseFactory = c => throw new ArgumentException(c.ModelState.ToErrors()); });
// builder.Services.AddApplicationServices();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddPersistenceServices(configuration);
//
// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel.Information() // Log seviyesini ayarla (Debug veya Information gibi) // default olarak Info olarak gelir
//     .Enrich.WithProperty("TraceId", Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString())
//     .WriteTo.Console()
//     .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Dosyaya yaz, loglama yapıalcak kaynakları belirtir
//     .WriteTo.MySQL(
//         configuration.GetConnectionString("LoggingBestPract"),
//         "logs",
//         restrictedToMinimumLevel: LogEventLevel.Information,    // minimum log seviyesi
//         storeTimestampInUtc: true,  // timestamp'ın utc değeri ile saklanıp saklanmayacağını belirtir.
//         batchSize: 100) // her seferde yazılacak log sayısını belitir
//     .WriteTo.Seq("http://localhost:5341/#/events")
//     .CreateLogger();
// builder.Host.UseSerilog();
//
// // http istek ve yanıtlarını loglamak için kullanılır
// builder.Services.AddHttpLogging(logging =>
// {
//     logging.LoggingFields = HttpLoggingFields.All; // hangi http alanlarının loglanacağını belirtir. response / request / all
//     logging.RequestHeaders.Add("sec-ch-ua"); // kullanıcıya dair tüm bilgileri getirir
//     logging.ResponseHeaders.Add("BestPract"); // uygulama adı
//     logging.MediaTypeOptions.AddText("application/json"); // JSON içeriklerini loglar.
//     logging.RequestBodyLogLimit = 4096; // maks boyut 4096 byte
//     logging.ResponseBodyLogLimit = 4096;
// });
//
// builder.Services.AddSwaggerGen();
// // builder.Host.UseCustomSerilog(configuration, "Stargate");
//
// builder.Services.AddHttpContextAccessor();
// builder.Services.AddTransient<ExceptionMiddleware>();
//
// builder.Host.UseSerilog();
//
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Youtube API", Version = "v1", Description = "Youtube API swagger client." });
// });
//
// var app = builder.Build();
//
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseSerilogRequestLogging();
// app.UseHttpLogging();
// app.UseMiddleware<TraceIdMiddleware>(); 
// app.UseMiddleware<ExceptionMiddleware>();
// app.UseMiddleware<LoggingMiddleware>();
// app.UseHttpsRedirection();
// app.UseRouting();
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
//
// app.ConfigureExceptionHandlingMiddleware();
// app.UseAuthorization();
//
// app.MapControllers();
//
// app.Run();

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using NLog.Extensions.Logging;
using LoggingBestPract.Api.Middlewares;
using LoggingBestPract.Application.Exceptions;
using LoggingBestPract.Application.Extensions;
using LoggingBestPract.Persistence.Extensions;
using LoggingBestPract.WebAPI.Extensions;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    

try
{
    logger.Debug("Application starting up...");
    var builder = WebApplication.CreateBuilder(args);

    var configurationBuilderBuilder = new ConfigurationBuilder();
    configurationBuilderBuilder
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);

    IConfigurationRoot configuration = configurationBuilderBuilder.Build();

    // Add NLog as the logging provider
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddControllers();
    builder.Services.AddTransient<ExceptionMiddleware>();
    builder.Services.Configure<ApiBehaviorOptions>(o => 
    { 
        o.InvalidModelStateResponseFactory = c => throw new ArgumentException(c.ModelState.ToErrors()); 
    });
    builder.Services.AddApplicationServices();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddPersistenceServices(configuration);

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Youtube API", Version = "v1", Description = "Youtube API swagger client." });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Middleware and routing
    app.UseMiddleware<TraceIdMiddleware>();
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<LoggingMiddleware>();
    app.UseHttpsRedirection();
    app.UseHttpLogging();
    app.UseRouting();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application stopped because of an exception");
    throw;
}
finally
{
    LogManager.Shutdown(); // Ensure to flush and stop internal timers/threads before application exit
}
