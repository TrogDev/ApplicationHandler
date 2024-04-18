namespace ApplicationHandler.Application.Applications.Commands.UpdateApplicationDepartment;

using ApplicationHandler.Application.Common.Exceptions;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Обновление отдела у запроса
public class UpdateApplicationDepartmentCommandHandler : IRequestHandler<UpdateApplicationDepartmentCommand>
{
    private readonly IApplicationDbContext context;

    public UpdateApplicationDepartmentCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(
        UpdateApplicationDepartmentCommand request,
        CancellationToken cancellationToken
    )
    {
        Application? application = await context.Applications.FirstOrDefaultAsync(
            e => e.Id == request.Id
        );

        if (application == null)
        {
            throw new EntityNotFoundException();
        }

        application.DepartmentId = request.DepartmentId;

        await context.SaveChangesAsync(cancellationToken);
    }
}
