namespace ApplicationHandler.Application.Users.Commands.UpdateCurrentUser;

using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Обновление данных о текущем пользователя (почта, телефон)
public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateCurrentUserCommand>
{
    private readonly IUser currentUser;
    private readonly IApplicationDbContext context;

    public UpdateCurrentUserCommandHandler(IUser currentUser, IApplicationDbContext context)
    {
        this.currentUser = currentUser;
        this.context = context;
    }

    public async Task Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
    {
        User user = await context.Users.FirstAsync(e => e.Id == currentUser.Id);
        user.Email = request.Email;
        user.Phone = request.Phone;
        await context.SaveChangesAsync();
    }
}
