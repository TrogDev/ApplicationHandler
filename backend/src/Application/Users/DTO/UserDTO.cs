using ApplicationHandler.Domain.Enums;

namespace ApplicationHandler.Application.Users.DTO;

public record UserDTO
{
    public required long Id { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required UserRole Role { get; init; }
}
