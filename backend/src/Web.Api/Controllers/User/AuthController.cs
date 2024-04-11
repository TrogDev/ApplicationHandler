using ApplicationHandler.Infrastructure.Auth.Commands.LogIn;
using ApplicationHandler.Infrastructure.Auth.Commands.Register;
using ApplicationHandler.Infrastructure.Auth.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationHandler.Web.Api.Controllers.User;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender mediator;

    public AuthController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromForm] LogInCommand command)
    {
        try {
            return Ok(await mediator.Send(command));
        }
        catch (InvalidLoginException) {
            return Unauthorized("Invalid login or password");
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromForm] RegisterCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}
