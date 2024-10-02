using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LoggingBestPract.Application.Interfaces.Logging;
using LoggingBestPract.Application.Interfaces.Repositories;
using LoggingBestPract.Domain.Contexts;
using LoggingBestPract.Persistence.Logging;
using LoggingBestPract.Persistence.Repositories.MySQL;

namespace LoggingBestPract.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlDependencies(configuration);
        services.AddScoped<ILoggingHolder, LoggingHolder>();
    }

    public static IServiceCollection AddSqlDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LoggingBestPract");

        return services
            .AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseMySQL(connectionString ?? throw new InvalidOperationException());
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            })
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IBrandRepository, BrandRepository>()
            .AddScoped<IProductRepository, ProductRepository>();
    }
}