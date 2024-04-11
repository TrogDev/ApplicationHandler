namespace ApplicationHandler.Application.Applications.Queries.GetApplications;

using ApplicationHandler.Application.Applications.DTO;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Application.Departments.DTO;
using ApplicationHandler.Application.Users.DTO;
using ApplicationHandler.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Получение всех запросов в системе, доступно только администраторам
public class GetUserApplicationsQueryHandler
    : IRequestHandler<GetApplicationsQuery, ICollection<ApplicationDTO>>
{
    private readonly IApplicationDbContext context;

    public GetUserApplicationsQueryHandler(IUser currentUser, IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<ICollection<ApplicationDTO>> Handle(
        GetApplicationsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = context
            .Applications.Include(e => e.Department)
            .Include(e => e.User)
            .OrderByDescending(e => e.CreatedAt)
            .Select(
                e =>
                    new ApplicationDTO()
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Description = e.Description,
                        User = new UserDTO()
                        {
                            Id = e.User.Id,
                            Email = e.User.Email,
                            Phone = e.User.Phone,
                            Role = e.User.Role
                        },
                        Status = e.Status,
                        Department = new DepartmentDTO()
                        {
                            Id = e.Department.Id,
                            Title = e.Department.Title
                        }
                    }
            );

        if (request.DepartmentId != null) {
            // Фильтрация по отделу
            query = query.Where(e => e.Department.Id == request.DepartmentId);
        }
        if (request.Status != null) {
            // Фильтрация по статусу
            query = query.Where(e => e.Status == request.Status);
        }

        return await query.ToListAsync(cancellationToken);
    }
}
