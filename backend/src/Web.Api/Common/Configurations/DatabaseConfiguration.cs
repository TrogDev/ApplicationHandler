using ApplicationHandler.Infrastructure.Data;

namespace ApplicationHandler.Web.Api.Common.Configurations;

public static class DatabaseConfiguration
{
    public static IApplicationBuilder ConfigureDatabase(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        var initialiser =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        initialiser.Initialize();

        return app;
    }
}
