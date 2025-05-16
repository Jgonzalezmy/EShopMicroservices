namespace Ordering.Domain.Abstractions
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {
        IDomainEvent[] ClearDomainEvents();
    }

    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }
}
