using Microsoft.Extensions.Logging;
using Sadin.Common.CustomExceptions;
using Sadin.Common.MediatRCommon.Commands;
using Sadin.Common.Utilities;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Users.Register;

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(IUnitOfWork uow,
        ILogger<RegisterCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        string hashedPassword = SecurityHelper.GetSha256Hash(request.Password);
        
        User user = User.Create(Guid.NewGuid(), request.UserName, hashedPassword, request.Email, request.PhoneNumber);
            
        Role? role = await _uow.Roles.GetByRoleName("User", cancellationToken);
        if (role == null)
            throw new KsNotFoundException("Role not found");

        try
        {
            user.AssignRoles(new[] { role });
            
            await _uow.Users.AddAsync(user, cancellationToken);
            
            await _uow.CommitAsync(cancellationToken);
            
            return user.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on adding user: {ErrorMessage}", ex.InnerException?.Message == null ? ex.Message : ex.InnerException.Message);
            throw;
        }
    }
}