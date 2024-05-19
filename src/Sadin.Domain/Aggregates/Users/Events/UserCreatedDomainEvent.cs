using Sadin.Common.Abstractions;

namespace Sadin.Domain.Aggregates.Users.Events;

public record UserCreatedDomainEvent(Guid Id) : IDomainEvent;