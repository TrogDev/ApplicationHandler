using ApplicationHandler.Domain.Enums;

namespace ApplicationHandler.Infrastructure.Auth.Common.Types;

public record AuthResponse
{
    public required long UserId { get; init; }
    public required UserRole Role { get; init; }
    public required JwtToken AccessToken { get; init; }

}
