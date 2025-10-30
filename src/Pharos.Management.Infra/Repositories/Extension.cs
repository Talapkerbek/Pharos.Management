using Microsoft.Extensions.DependencyInjection;

namespace Pharos.Management.Infra.Repositories;

public static class Extension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // services.AddScoped<IFeatureRepository, FeatureRepository>();
        
        return services;
    }
}