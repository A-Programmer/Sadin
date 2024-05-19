using MediatR;
using Microsoft.Extensions.Logging;
using Sadin.Domain.Aggregates.Users.Events;

namespace Sadin.Application.Users.Register.EventHandlers;

public sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly ILogger<UserCreatedDomainEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UserCreatedDomainEventHandler(ILogger<UserCreatedDomainEventHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UserCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        User? user = await _unitOfWork.Users.GetByIdAsync(notification.Id,
            cancellationToken);
        
        if (user is not null)
        {
            // TODO: Implement EmailSender service in the Infrastructure project and use it here.
            _logger.LogInformation("Sending email to UserID: {Id}", notification.Id);
        }
        else
        {
            _logger.LogError("User with ID {Id} could not be found to send email.", notification.Id);
        }
    }
}