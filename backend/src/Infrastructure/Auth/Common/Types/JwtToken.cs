namespace ApplicationHandler.Infrastructure.Auth.Common.Types;

public record JwtToken
{
    public required string Token { get; init; }
    public required int ExpiresIn { get; init; }
}
