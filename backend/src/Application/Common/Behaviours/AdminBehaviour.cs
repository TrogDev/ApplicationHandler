using ApplicationHandler.Application.Common.Exceptions;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ApplicationHandler.Application.Common.Behaviours;

public class AdminBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUser currentUser;

    public AdminBehaviour(IUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (request is IAdminRequest)
        {
            if (currentUser.Role != UserRole.Admin)
            {
                throw new ForbiddenException();
            }
        }
        return await next();
    }
}
