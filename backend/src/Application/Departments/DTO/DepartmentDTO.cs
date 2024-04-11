namespace ApplicationHandler.Application.Departments.DTO;

public record DepartmentDTO
{
    public required long Id { get; init; }
    public required string Title { get; init; }
}
