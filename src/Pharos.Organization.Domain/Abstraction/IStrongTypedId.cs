namespace Pharos.Organization.Domain.Abstraction;

public interface IStrongTypedId
{
    Guid Value { get; }
}