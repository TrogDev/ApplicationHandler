using ApplicationHandler.Application.Applications.Commands.UpdateApplicationDepartment;
using ApplicationHandler.Application.Applications.Commands.UpdateApplicationStatus;
using ApplicationHandler.Application.Applications.Queries.GetApplications;
using ApplicationHandler.Domain.Enums;
using ApplicationHandler.Infrastructure.Auth.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationHandler.Web.Api.Controllers.Admin;

[ApiController]
[Route("/admin/applications")]
public class ApplicationController : ControllerBase
{
    private readonly ISender mediator;

    public ApplicationController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetList([FromQuery] GetApplicationsQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpPut]
    [Authorize]
    [Route("{id:guid}/status")]
    public async Task<IActionResult> PutStatus([FromRoute] Guid id, [FromForm] ApplicationStatus status)
    {
        var command = new UpdateApplicationStatusCommand()
        {
            Id = id,
            Status = status
        };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut]
    [Authorize]
    [Route("{id:guid}/department")]
    public async Task<IActionResult> PutDepartment([FromRoute] Guid id, [FromForm] long departmentId)
    {
        var command = new UpdateApplicationDepartmentCommand()
        {
            Id = id,
            DepartmentId = departmentId
        };
        await mediator.Send(command);
        return NoContent();
    }
}
