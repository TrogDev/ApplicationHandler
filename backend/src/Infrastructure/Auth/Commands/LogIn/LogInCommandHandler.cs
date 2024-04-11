namespace ApplicationHandler.Infrastructure.Auth.Commands.LogIn;

using Microsoft.EntityFrameworkCore;

using MediatR;

using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Infrastructure.Auth.Common.Exceptions;
using ApplicationHandler.Infrastructure.Auth.Common.Interfaces;
using ApplicationHandler.Infrastructure.Auth.Common.Types;

public class LogInCommandHandler : IRequestHandler<LogInCommand, AuthResponse>
{
    private readonly IAuthDbContext context;
    private readonly ITokenService tokenService;
    private readonly IHasher hasher;

    public LogInCommandHandler(IAuthDbContext context, IHasher hasher, ITokenService tokenService)
    {
        this.context = context;
        this.hasher = hasher;
        this.tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(
        LogInCommand request,
        CancellationToken cancellationToken
    )
    {
        User user = await findUser(request.Email, request.Password);
        return new AuthResponse()
        {
            UserId = user.Id,
            Role = user.Role,
            AccessToken = tokenService.CreateAccessToken(user)
        };
    }

    private async Task<User> findUser(string email, string password)
    {
        string passwordHash = hasher.Hash(password);
        User? user = await context.Users.FirstOrDefaultAsync(
            e => e.Email == email && e.PasswordHash == passwordHash
        );

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        return user;
    }
}