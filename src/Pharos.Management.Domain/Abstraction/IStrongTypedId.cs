namespace Pharos.Management.Domain.Abstraction;

public interface IStrongTypedId
{
    Guid Value { get; }
}