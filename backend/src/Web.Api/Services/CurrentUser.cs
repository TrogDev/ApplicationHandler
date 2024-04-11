using System.Security.Claims;
using ApplicationHandler.Application.Common.Exceptions;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Enums;

namespace ApplicationHandler.Web.Api.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public long Id => long.Parse(getCurrentUser().FindFirstValue(ClaimTypes.NameIdentifier)!);

    public UserRole Role =>
        (UserRole)Enum.Parse(typeof(UserRole), getCurrentUser().FindFirstValue(ClaimTypes.Role)!);

    private ClaimsPrincipal getCurrentUser()
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
        if (user is null)
            throw new UnauthorizedException();
        return user;
    }
}
