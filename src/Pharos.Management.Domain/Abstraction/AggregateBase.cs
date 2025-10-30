using System.Text.Json.Serialization;

namespace Pharos.Management.Domain.Abstraction;

public abstract class AggregateBase<T> : IEntity<T> where T : IStrongTypedId
{
    [JsonInclude] public T Id { get; protected set; }
    public long Version { get; protected set; }
    private readonly List<IDomainEvent> _uncommittedEvents  = new();
    
    public void AddUncommittedEvent(IDomainEvent @event)
    {
        _uncommittedEvents.Add(@event);
    }
    
    public IEnumerable<IDomainEvent> GetUncommittedEvents()
    {
        return _uncommittedEvents;
    }

    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    }
}