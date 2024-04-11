using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Application.Departments.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationHandler.Application.Departments.Queries.GetDepartments;

//Получение списка отделов
public class GetDepartmentsQueryHandler
    : IRequestHandler<GetDepartmentsQuery, ICollection<DepartmentDTO>>
{
    private readonly IApplicationDbContext context;

    public GetDepartmentsQueryHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<ICollection<DepartmentDTO>> Handle(
        GetDepartmentsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await context
            .Departments.Select(e => new DepartmentDTO() { Id = e.Id, Title = e.Title })
            .ToListAsync();
    }
}
