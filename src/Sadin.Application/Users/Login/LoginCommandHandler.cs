using System.Security.Authentication;
using Sadin.Common.CustomExceptions;
using Sadin.Common.Utilities;

namespace Sadin.Application.Users.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly IJwtService _jwtService; 

    public LoginCommandHandler(IUnitOfWork uow,
        IJwtService jwtService)
    {
        _uow = uow;
        _jwtService = jwtService;
    }
    
    public async Task<LoginResponse> Handle(LoginCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _uow.Users.FindUserByUserName(request.UserName,
            cancellationToken);
        if (user is null)
            throw new KsNotFoundException(request.UserName);

        string hashedPassword = SecurityHelper.GetSha256Hash(request.Password);
        if (user.HashedPassword != hashedPassword)
            throw new AuthenticationException("Username or password is not correct.");

        return new LoginResponse(_jwtService.GenerateToken(user));
    }
}