namespace ApplicationHandler.Infrastructure.Auth.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Infrastructure.Auth.Common.Interfaces;
using ApplicationHandler.Infrastructure.Auth.Common.Options;
using ApplicationHandler.Infrastructure.Auth.Common.Types;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    private readonly JwtOptions options;

    public TokenService(JwtOptions options)
    {
        this.options = options;
    }

    public JwtToken CreateAccessToken(User user)
    {
        List<Claim> claims = createClaims(user);
        string token = createToken(claims);
        return new JwtToken()
        {
            Token = token,
            ExpiresIn = (int)TimeSpan.FromDays(options.AccessLifeTimeDays).TotalSeconds
        };
    }

    private List<Claim> createClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        return claims;
    }

    private string createToken(List<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(options.AccessLifeTimeDays)),
            signingCredentials: new SigningCredentials(
                options.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            )
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
