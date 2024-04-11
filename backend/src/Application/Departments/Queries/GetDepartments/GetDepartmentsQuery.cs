using ApplicationHandler.Application.Departments.DTO;
using MediatR;

namespace ApplicationHandler.Application.Departments.Queries.GetDepartments;

public record GetDepartmentsQuery : IRequest<ICollection<DepartmentDTO>> { }
