using Microsoft.EntityFrameworkCore;
using FluentValidation;

using ApplicationHandler.Infrastructure.Auth.Common.Interfaces;

namespace ApplicationHandler.Infrastructure.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IAuthDbContext context;

    public RegisterCommandValidator(IAuthDbContext context)
    {
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

        RuleFor(e => e.Password).MinimumLength(8).NotEmpty();
    }

    private bool isValidPhone(string phone)
    {
        return phone.StartsWith('7') && phone.All(char.IsDigit) && phone.Length == 11;
    }

    private async Task<bool> isPhoneUnique(string phone, CancellationToken token)
    {
        return !await context.Users.AnyAsync(
            e => e.Phone == phone,
            token
        );
    }

    private async Task<bool> isEmailUnique(string email, CancellationToken token)
    {
        return !await context.Users.AnyAsync(e => e.Email.ToLower() == email.ToLower(), token);
    }
}