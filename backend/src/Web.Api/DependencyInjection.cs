using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Web.Api.Middlewares;
using ApplicationHandler.Web.Api.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUser, CurrentUser>();
        services.AddScoped<ValidationMiddleware>();
        services.AddScoped<ForbiddenMiddleware>();
        return services;
    }
}
