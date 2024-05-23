using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sadin.Common;

namespace Sadin.Application.Services;

public sealed class JwtService : IJwtService
{
    private readonly PublicSettings _settings;

    public JwtService(IOptionsSnapshot<PublicSettings> settings)
    {
        _settings = settings.Value ??
                    throw new ArgumentNullException(nameof(settings));
    }

    public string GenerateToken(User user)
    {
        var secretKey = Encoding.UTF8.GetBytes(_settings.JwtOptions.SecretKey);

        var signInCredentials =
            new SigningCredentials(new SymmetricSecurityKey(secretKey),
                                    SecurityAlgorithms.HmacSha256Signature);

        var claims = _getClaims(user);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _settings.JwtOptions.Issuer,
            Audience = _settings.JwtOptions.Audience,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow.AddMinutes(_settings.JwtOptions.NotBeforeInMinutes),
            Expires = DateTime.UtcNow.AddMinutes(_settings.JwtOptions.ExpirationInMinutes),
            SigningCredentials = signInCredentials,
            Subject = new ClaimsIdentity(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(descriptor);

        return tokenHandler.WriteToken(token);
    }
    
    private IEnumerable<Claim> _getClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        if (user.Roles.Any())
        {
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name.ToLower()));
            }
        }

        return claims;
    }

}