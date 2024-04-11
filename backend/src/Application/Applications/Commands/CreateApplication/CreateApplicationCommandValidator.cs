using ApplicationHandler.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApplicationHandler.Application.Applications.Commands.CreateApplication;

public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
{
    private readonly IApplicationDbContext context;

    public CreateApplicationCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.Title).NotEmpty();
        RuleFor(e => e.Description).NotEmpty();
    }
}
