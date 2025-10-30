using Microsoft.Extensions.DependencyInjection;

namespace Pharos.Management.Domain.DomainServices;

public static class Extensions
{
    public static IServiceCollection AddDomainServices
    (
        this IServiceCollection services
    )
    {
        // services.AddScoped<IFeatureService, FeatureService>();
        
        return services;
    }
}