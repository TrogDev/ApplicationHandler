using ApplicationHandler.Domain.Common;
using ApplicationHandler.Domain.Enums;

namespace ApplicationHandler.Domain.Entities;

// Модель клиента
public class User : BaseEntity<long>
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; } = UserRole.Client;
}
