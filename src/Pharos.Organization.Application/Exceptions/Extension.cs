using Microsoft.Extensions.DependencyInjection;

namespace Pharos.Organization.Application.Exceptions;

public static class Extension
{
    public static IServiceCollection ConfigureExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ExceptionHandler>();
        
        return services;
    }
}