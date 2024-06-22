using Sadin.Common.CustomExceptions;

namespace Sadin.Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery,
    UserResponse>
{
    private readonly IUnitOfWork _uow;

    public GetUserByIdQueryHandler(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }
    
    public async Task<UserResponse> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        User user = await _uow.Users.FindUserWithRolesAsync(request.Id, cancellationToken);

        if (user is null)
            throw new KsNotFoundException(request.Id.ToString());

        return new UserResponse(user.Id,
            user.UserName, user.Email, user.PhoneNumber,
            user.Roles.Select(r => r.Name).ToList());
    }
}