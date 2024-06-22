using Microsoft.Extensions.Logging;
using Sadin.Common.CustomExceptions;
using Sadin.Common.Utilities;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Users.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(IUnitOfWork uow,
        ILogger<CreateUserCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await CheckIfUserExist(request, cancellationToken))
            throw new KsDuplicatedUserException("A user with the same provided information is exist.");
        
        string hashedPassword = SecurityHelper.GetSha256Hash(request.Password);
        
        User user = User.Create(Guid.NewGuid(), request.UserName, hashedPassword, request.Email, request.PhoneNumber);

        List<Role> roles = new();

        foreach (string roleName in request.Roles)
        {
            Role? role = await _uow.Roles.GetByRoleName(roleName, cancellationToken);
            
            if (role == null)
                throw new KsNotFoundException("Role not found");
            
            roles.Add(role);
        }

        try
        {
            user.AssignRoles(roles);
            
            await _uow.Users.AddAsync(user, cancellationToken);
            
            await _uow.CommitAsync(cancellationToken);
            
            return new(user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on adding user: {ErrorMessage}", ex.InnerException?.Message == null ? ex.Message : ex.InnerException.Message);
            throw;
        }
    }

    private async Task<bool> CheckIfUserExist(CreateUserCommand request,
        CancellationToken cancellationToken = default)
    {
        return 
            (await _uow.Users.FindUserByUserNameAsync(request.UserName, cancellationToken) is not null) ||
            (await _uow.Users.FindUserByEmailAsync(request.Email, cancellationToken) is not null) ||
            (await _uow.Users.FindUserByPhoneNumber(request.PhoneNumber, cancellationToken) is not null);
    }
}