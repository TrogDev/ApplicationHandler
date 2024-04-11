using MediatR;

using ApplicationHandler.Infrastructure.Auth.Common.Types;

namespace ApplicationHandler.Infrastructure.Auth.Commands.LogIn;

public record LogInCommand : IRequest<AuthResponse>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
