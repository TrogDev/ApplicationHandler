using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Infrastructure.Auth.Common.Types;

namespace ApplicationHandler.Infrastructure.Auth.Common.Interfaces;

public interface ITokenService
{
    JwtToken CreateAccessToken(User user);
}
