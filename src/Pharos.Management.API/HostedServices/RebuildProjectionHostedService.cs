using Marten;

namespace Pharos.Management.API.HostedServices;

public class RebuildProjectionHostedService(IServiceProvider serviceProvider, ILogger<RebuildProjectionHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting RebuildProjectionHostedService");
        
        using var scope = serviceProvider.CreateScope();
        var store = scope.ServiceProvider.GetRequiredService<IDocumentStore>();

        using var rebuilder = await store.BuildProjectionDaemonAsync();
        // await rebuilder.RebuildProjectionAsync<FeatureReadModel>(cancellationToken);
        
        logger.LogInformation("RebuildProjectionHostedService finished");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}