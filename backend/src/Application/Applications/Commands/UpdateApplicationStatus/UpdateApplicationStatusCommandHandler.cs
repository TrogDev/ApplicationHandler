namespace ApplicationHandler.Application.Applications.Commands.UpdateApplicationStatus;

using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ApplicationHandler.Application.Common.Exceptions;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Обновление статуса у запроса (принятие, отклонение)
public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatusCommand>
{
    private readonly IApplicationDbContext context;

    public UpdateApplicationStatusCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(
        UpdateApplicationStatusCommand request,
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

        application.Status = request.Status;

        await context.SaveChangesAsync(cancellationToken);
    }
}
