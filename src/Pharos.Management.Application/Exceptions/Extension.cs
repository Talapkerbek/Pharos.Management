using Microsoft.Extensions.DependencyInjection;

namespace Pharos.Management.Application.Exceptions;

public static class Extension
{
    public static IServiceCollection ConfigureExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ExceptionHandler>();
        
        return services;
    }
}