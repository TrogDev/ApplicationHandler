using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Enums;
using MediatR;

namespace ApplicationHandler.Application.Applications.Commands.UpdateApplicationDepartment;

public class UpdateApplicationDepartmentCommand : IRequest, IAdminRequest
{
    public required Guid Id { get; init; }
    public required long DepartmentId { get; init; }
}
