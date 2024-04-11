using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApplicationHandler.Infrastructure.Auth.Common.Options;

public record JwtOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string PrivateKey { get; init; }
    public required int AccessLifeTimeDays { get; init; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PrivateKey));
    }
}
