using Marten;
using Pharos.Management.Domain.Abstraction;

namespace Pharos.Management.Infra.Repositories;

public abstract class RepositoryBase<TAggregate, TId>(IDocumentSession session)
    where TAggregate : AggregateBase<TId>
    where TId : IStrongTypedId
{
    public async Task StoreAsync(TAggregate aggregate, CancellationToken ct = default)
    {
        var events = aggregate.GetUncommittedEvents();
        session.Events.Append(aggregate.Id.Value, aggregate.Version, events);
        await session.SaveChangesAsync(ct);
        aggregate.ClearUncommittedEvents();
    }

    public async Task<T> LoadAsync<T>(
        Guid id,
        CancellationToken ct = default
    ) where T : AggregateBase<TId>
    {
        var aggregate = await session.Events.AggregateStreamAsync<T>(id, token: ct);
        return aggregate ?? throw new InvalidOperationException($"No aggregate by id {id}.");
    }
}