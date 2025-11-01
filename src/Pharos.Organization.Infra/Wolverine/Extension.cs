using System.Reflection;
using JasperFx;
using JasperFx.Core;
using JasperFx.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.Kafka;

namespace Pharos.Organization.Infra.Wolverine;

public static class Extensions
{
    public static IHostBuilder AddWolverineWithAssemblyDiscovery
    (
        this IHostBuilder host,
        IConfiguration configuration,
        List<Assembly> assemblies
    )
    {

        host.UseWolverine(opts =>
        {
            // If we encounter a concurrency exception, just try it immediately
            // up to 3 times total
            opts.Policies.OnException<ConcurrencyException>().RetryTimes(3);

            // It's an imperfect world, and sometimes transient connectivity errors
            // to the database happen
            opts.Policies.OnException<NpgsqlException>()
                .RetryWithCooldown(50.Milliseconds(), 100.Milliseconds(), 250.Milliseconds());

            opts.UseKafka("localhost:9094").AutoProvision();

            // opts.PublishMessage<UserCreatedEvent>().ToKafkaTopic("UserCreatedEvent");
            
            foreach (var assembly in assemblies)
            {
                opts.Discovery.IncludeAssembly(assembly);
            }

            // Adding outbox on all publish
            opts.Policies.UseDurableOutboxOnAllSendingEndpoints();

            // Enrolling all local queues into the
            // durable inbox/outbox processing
            opts.Policies.UseDurableLocalQueues();

            // Adding inbox on all consumers
            opts.Policies.UseDurableInboxOnAllListeners();

            // Auto applying transactions, calls SaveChangesAsync auto-ly after handler ending 
            opts.Policies.AutoApplyTransactions();

            host.UseResourceSetupOnStartup();
        });

        return host;
    }
}