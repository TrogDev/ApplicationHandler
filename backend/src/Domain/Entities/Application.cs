using ApplicationHandler.Domain.Common;
using ApplicationHandler.Domain.Enums;

namespace ApplicationHandler.Domain.Entities;

// Модель запроса клиента
public class Application : BaseEntity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long DepartmentId { get; set; }
    public Department Department { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
