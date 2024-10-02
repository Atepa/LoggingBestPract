using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using LoggingBestPract.Application.Interfaces.Repositories;
using LoggingBestPract.Persistence.Repositories.MySQL;


namespace LoggingBestPract.Application.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}