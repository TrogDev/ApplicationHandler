using ApplicationHandler.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApplicationHandler.Application.Applications.Commands.CreateApplication;

public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationCommandValidator(IApplicationDbContext context)
    {
        RuleFor(e => e.Title).NotEmpty();
        RuleFor(e => e.Description).NotEmpty();
    }
}
