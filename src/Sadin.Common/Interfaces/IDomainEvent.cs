namespace Sadin.Common.Interfaces;

public interface IDomainEvent : INotification
{
    DateTimeOffset OccuredOn { get; }
}