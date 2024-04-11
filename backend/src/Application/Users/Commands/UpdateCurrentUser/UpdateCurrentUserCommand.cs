using MediatR;

namespace ApplicationHandler.Application.Users.Commands.UpdateCurrentUser;

public class UpdateCurrentUserCommand : IRequest
{
    public required string Email { get; init; }
    public required string Phone { get; init; }
}
