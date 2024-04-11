using MediatR;

namespace ApplicationHandler.Application.Applications.Commands.CreateApplication;

public class CreateApplicationCommand : IRequest<Guid>
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}
