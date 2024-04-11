using ApplicationHandler.Application.Users.Commands.UpdateCurrentUser;
using ApplicationHandler.Application.Users.Queries.GetCurrentUser;
using ApplicationHandler.Infrastructure.Auth.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationHandler.Web.Api.Controllers.User;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly ISender mediator;

    public UserController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [Route("me")]
    public async Task<IActionResult> GetCurrent()
    {
        var query = new GetCurrentUserQuery();
        return Ok(await mediator.Send(query));
    }

    [HttpPut]
    [Authorize]
    [Route("me")]
    public async Task<IActionResult> UpdateCurrent([FromForm] UpdateCurrentUserCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
