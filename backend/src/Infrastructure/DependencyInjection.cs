using System.Reflection;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Infrastructure.Auth.Common.Interfaces;
using ApplicationHandler.Infrastructure.Auth.Common.Options;
using ApplicationHandler.Infrastructure.Auth.Services;
using ApplicationHandler.Infrastructure.Data;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.addDatabaseServices(configuration);
        services.addAuthServices(configuration);

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }

    private static void addDatabaseServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string connection = configuration.GetConnectionString("Default")!;

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));
        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>()
        );
        services.AddScoped<IAuthDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>()
        );
        services.AddScoped<ApplicationDbContextInitializer>();
    }

    private static void addAuthServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        JwtOptions options = configuration.GetRequiredSection("JwtAuthOptions").Get<JwtOptions>()!;

        services.AddSingleton(options);
        services.AddScoped<IHasher, Sha256Hasher>();
        services.AddScoped<ITokenService, TokenService>();
        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,
                    ValidateAudience = true,
                    ValidAudience = options.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = options.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
    }
}
