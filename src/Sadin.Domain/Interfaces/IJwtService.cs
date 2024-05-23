using System.IdentityModel.Tokens.Jwt;
using Sadin.Domain.Aggregates.Users;

namespace Sadin.Domain.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}