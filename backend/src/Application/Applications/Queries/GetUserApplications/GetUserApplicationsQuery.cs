using ApplicationHandler.Application.Applications.DTO;
using MediatR;

namespace ApplicationHandler.Application.Applications.Queries.GetUserApplications;

public record GetUserApplicationsQuery : IRequest<ICollection<ApplicationDTO>> { }
