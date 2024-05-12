using Sadin.Common.MediatRCommon.Commands;

namespace Sadin.Application.Users.Login;

public sealed record LoginCommand(string UserName,
    string Password) : ICommand<LoginResponse>;