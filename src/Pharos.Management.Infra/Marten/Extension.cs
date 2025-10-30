using JasperFx;
using JasperFx.Events.Projections;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolverine.Marten;

namespace Pharos.Management.Infra.Marten;

public static class Extensions
{
    public static IServiceCollection AddAndConfigureMarten
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMarten(opts =>
            {
                opts.Connection(configuration.GetConnectionString("PostgreSQL")!);
                opts.Events.DatabaseSchemaName = "events";
                opts.AutoCreateSchemaObjects = AutoCreate.All;

                // opts.Schema.For<FeatureReadModel>();
                // opts.Projections.Add<FeatureProjection>(ProjectionLifecycle.Inline);
            })
            .IntegrateWithWolverine();

        return services;
    }
}