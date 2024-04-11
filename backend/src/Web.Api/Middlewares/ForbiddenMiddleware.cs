namespace ApplicationHandler.Web.Api.Middlewares;

using Microsoft.AspNetCore.Mvc;

using ApplicationHandler.Application.Common.Exceptions;

public class ForbiddenMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ForbiddenException)
        {
            var response = new ObjectResult("Forbidden for you") { StatusCode = 403 };
            await response.ExecuteResultAsync(new ActionContext { HttpContext = context });
        }
    }
}
