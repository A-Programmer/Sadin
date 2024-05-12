using System.IdentityModel.Tokens.Jwt;

namespace Sadin.Application.Users.Login;

public record LoginResponse(string Token);