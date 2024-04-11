using Microsoft.EntityFrameworkCore;

using MediatR;

using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Infrastructure.Auth.Common.Interfaces;
using ApplicationHandler.Infrastructure.Auth.Common.Types;

namespace ApplicationHandler.Infrastructure.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IAuthDbContext context;
    private readonly ITokenService tokenService;
    private readonly IHasher hasher;

    public RegisterCommandHandler(
        IAuthDbContext context,
        IHasher hasher,
        ITokenService tokenService
    )
    {
        this.context = context;
        this.hasher = hasher;
        this.tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken
    )
    {
        User user = await createUser(request);
        return new AuthResponse()
        {
            UserId = user.Id,
            Role = user.Role,
            AccessToken = tokenService.CreateAccessToken(user)
        };
    }

    private async Task<User> createUser(RegisterCommand request)
    {
        var user = new User()
        {
            Email = request.Email,
            Phone = request.Phone,
            PasswordHash = hasher.Hash(request.Password)
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return user;
    }
}