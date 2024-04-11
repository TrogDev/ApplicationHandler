using ApplicationHandler.Application.Departments.Queries.GetDepartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationHandler.Web.Api.Controllers.User;

[ApiController]
[Route("/departments")]
public class DepartmentController : ControllerBase
{
    private readonly ISender mediator;

    public DepartmentController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var query = new GetDepartmentsQuery();
        return Ok(await mediator.Send(query));
    }
}
