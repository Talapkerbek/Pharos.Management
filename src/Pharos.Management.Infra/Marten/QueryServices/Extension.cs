using Microsoft.Extensions.DependencyInjection;

namespace Pharos.Management.Infra.Marten.QueryServices;

public static class Extension
{
    public static IServiceCollection AddQueryServices(this IServiceCollection services)
    {
        // services.AddScoped<IFeatureQueryService, FeatureQueryService>();
        
        return services;
    }
}