using ApplicationHandler.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApplicationHandler.Application.Applications.Commands.UpdateApplicationDepartment;

public class UpdateApplicationDepartmentCommandValidator : AbstractValidator<UpdateApplicationDepartmentCommand>
{
    private readonly IApplicationDbContext context;

    public UpdateApplicationDepartmentCommandValidator(IApplicationDbContext context)
    {
        this.context = context;

        RuleFor(e => e.DepartmentId).MustAsync(isDepartmentExists).WithMessage("Отдел не найден");
    }

    private async Task<bool> isDepartmentExists(long id, CancellationToken token)
    {
        return await context.Departments.AnyAsync(e => e.Id == id);
    }
}
