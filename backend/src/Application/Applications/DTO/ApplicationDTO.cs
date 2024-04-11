using ApplicationHandler.Application.Departments.DTO;
using ApplicationHandler.Application.Users.DTO;
using ApplicationHandler.Domain.Enums;

namespace ApplicationHandler.Application.Applications.DTO;

public record ApplicationDTO
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required UserDTO User { get; init; }
    public required ApplicationStatus Status { get; init; }
    public required DepartmentDTO Department { get; init; }
}
