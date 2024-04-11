using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Enums;
using MediatR;

namespace ApplicationHandler.Application.Applications.Commands.UpdateApplicationStatus;

public class UpdateApplicationStatusCommand : IRequest, IAdminRequest
{
    public required Guid Id { get; init; }
    public required ApplicationStatus Status { get; init; }
}
