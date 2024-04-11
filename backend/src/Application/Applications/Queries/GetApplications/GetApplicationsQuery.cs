using ApplicationHandler.Application.Applications.DTO;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Enums;
using MediatR;

namespace ApplicationHandler.Application.Applications.Queries.GetApplications;

public record GetApplicationsQuery : IRequest<ICollection<ApplicationDTO>>, IAdminRequest
{
    public long? DepartmentId { get; init; }
    public ApplicationStatus? Status { get; init; }
}
