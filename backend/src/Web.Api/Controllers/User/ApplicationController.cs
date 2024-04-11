using ApplicationHandler.Application.Applications.Commands.CreateApplication;
using ApplicationHandler.Application.Applications.Queries.GetUserApplications;
using ApplicationHandler.Application.Users.Commands.UpdateCurrentUser;
using ApplicationHandler.Application.Users.Queries.GetCurrentUser;
using ApplicationHandler.Infrastructure.Auth.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationHandler.Web.Api.Controllers.User;

[ApiController]
[Route("/applications")]
public class ApplicationController : ControllerBase
{
    private readonly ISender mediator;

    public ApplicationController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetList()
    {
        var query = new GetUserApplicationsQuery();
        return Ok(await mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromForm] CreateApplicationCommand command)
    {
        return StatusCode(201, await mediator.Send(command));
    }
}
