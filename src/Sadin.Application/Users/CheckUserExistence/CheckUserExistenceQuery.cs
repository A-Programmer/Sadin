namespace Sadin.Application.Users.CheckUserExistence;

public sealed record CheckUserExistenceQuery(string UserName) : IQuery<Guid?>;