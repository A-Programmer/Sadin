namespace Sadin.Application.Users.CheckUserExistence;

public sealed class CheckUserExistenceQueryHandler : IQueryHandler<CheckUserExistenceQuery, Guid?>
{
    private readonly IUnitOfWork _uow;

    public CheckUserExistenceQueryHandler(IUnitOfWork uow) => _uow = uow;
    
    public async Task<Guid?> Handle(CheckUserExistenceQuery request, CancellationToken cancellationToken)
    {
        bool userByUserName = false;
        bool userByEmail = false;
        bool userByPhoneNumber = false;

        User? user = await _uow.Users.FindUserByUserNameAsync(request.UserName, cancellationToken);
        if (user is not null)
            return user.Id;
         
        user = await _uow.Users.FindUserByEmailAsync(request.UserName, cancellationToken);
        if (user is not null)
            return user.Id;
        
        user = await _uow.Users.FindUserByPhoneNumber(request.UserName, cancellationToken);
        if (user is not null)
            return user.Id;
        

        return null;
    }
}