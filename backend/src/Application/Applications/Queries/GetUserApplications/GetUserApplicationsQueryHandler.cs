namespace ApplicationHandler.Application.Applications.Queries.GetUserApplications;

using ApplicationHandler.Application.Applications.DTO;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Application.Departments.DTO;
using ApplicationHandler.Application.Users.DTO;
using ApplicationHandler.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Получение всех запросов пользователя
public class GetUserApplicationsQueryHandler
    : IRequestHandler<GetUserApplicationsQuery, ICollection<ApplicationDTO>>
{
    private readonly IUser currentUser;
    private readonly IApplicationDbContext context;

    public GetUserApplicationsQueryHandler(IUser currentUser, IApplicationDbContext context)
    {
        this.currentUser = currentUser;
        this.context = context;
    }

    public async Task<ICollection<ApplicationDTO>> Handle(
        GetUserApplicationsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await context.Applications
            .Include(e => e.Department)
            .Include(e => e.User)
            .Where(e => e.UserId == currentUser.Id)
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
            )
            .ToListAsync(cancellationToken);
    }
}
