using Microsoft.Extensions.Logging;
using Sadin.Common.CustomExceptions;
using Sadin.Common.Utilities;

namespace Sadin.Application.Users.ResetPassword;

public sealed class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;
    
    public ResetPasswordCommandHandler(IUnitOfWork uow, ILogger<ResetPasswordCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _uow.Users.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            throw new KsNotFoundException(request.Id.ToString());

        string hashedPassword = SecurityHelper.GetSha256Hash(request.NewPassword);
        
        user.UpdatePassword(hashedPassword);

        await _uow.CommitAsync(cancellationToken);
    }
}