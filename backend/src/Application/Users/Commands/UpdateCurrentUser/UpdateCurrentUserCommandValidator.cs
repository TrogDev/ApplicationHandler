namespace ApplicationHandler.Application.Users.Commands.UpdateCurrentUser;

using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class UpdateCurrentUserCommandValidator : AbstractValidator<UpdateCurrentUserCommand>
{
    private readonly IApplicationDbContext context;
    private readonly IUser currentUser;

    public UpdateCurrentUserCommandValidator(IApplicationDbContext context, IUser currentUser)
    {
        this.currentUser = currentUser;
        this.context = context;

        RuleFor(e => e.Phone)
            .NotEmpty()
            .MaximumLength(30)
            .Must(isValidPhone)
            .WithMessage("Некорректный номер телефона")
            .MustAsync(isPhoneUnique)
            .WithMessage("Пользователь с таким номером телефона уже существует");

        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(isEmailUnique)
            .WithMessage("Пользователь с такой почтой уже существует");
    }

    private bool isValidPhone(string phone)
    {
        return phone.StartsWith('7') && phone.All(char.IsDigit) && phone.Length == 11;
    }

    private async Task<bool> isPhoneUnique(string phone, CancellationToken token)
    {
        User? userWithPhone = await context.Users.FirstOrDefaultAsync(
            e => e.Phone == phone,
            token
        );

        if (userWithPhone == null)
        {
            return true;
        }
        else if (userWithPhone.Id == currentUser.Id)
        {
            return true;
        }

        return false;
    }

    private async Task<bool> isEmailUnique(string email, CancellationToken token)
    {
        User? userWithEmail = await context.Users.FirstOrDefaultAsync(
            e => e.Email.ToLower() == email.ToLower(),
            token
        );

        if (userWithEmail == null)
        {
            return true;
        }
        else if (userWithEmail.Id == currentUser.Id)
        {
            return true;
        }

        return false;
    }
}
