using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Application.Users.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationHandler.Application.Users.Queries.GetCurrentUser;

// Получение текущего пользователя
public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDTO>
{
    private readonly IUser currentUser;
    private readonly IApplicationDbContext context;

    public GetCurrentUserQueryHandler(IUser currentUser, IApplicationDbContext context)
    {
        this.currentUser = currentUser;
        this.context = context;
    }

    public async Task<UserDTO> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken
    )
    {
        return await context
            .Users.Select(
                e =>
                    new UserDTO()
                    {
                        Id = e.Id,
                        Email = e.Email,
                        Phone = e.Phone,
                        Role = e.Role
                    }
            )
            .FirstAsync(e => e.Id == currentUser.Id);
    }
}
