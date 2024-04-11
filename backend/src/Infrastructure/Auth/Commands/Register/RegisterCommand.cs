using MediatR;

using ApplicationHandler.Infrastructure.Auth.Common.Types;

namespace ApplicationHandler.Infrastructure.Auth.Commands.Register;

public record RegisterCommand : IRequest<AuthResponse>
{
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string Password { get; init; }
}
