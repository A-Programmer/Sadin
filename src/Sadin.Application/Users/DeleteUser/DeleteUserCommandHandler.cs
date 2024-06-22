using Microsoft.Extensions.Logging;
using Sadin.Common.CustomExceptions;

namespace Sadin.Application.Users.DeleteUser;

public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(IUnitOfWork uow,
        ILogger<DeleteUserCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _uow.Users.FindUserWithRolesAsync(request.Id, cancellationToken)  ??
                    throw new KsNotFoundException();
        
        // remove children
        user.ClearRoles();
        
        _uow.Users.Remove(user);
        
        await _uow.CommitAsync(cancellationToken);
    }
}