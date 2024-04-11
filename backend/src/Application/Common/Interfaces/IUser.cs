using ApplicationHandler.Domain.Enums;

namespace ApplicationHandler.Application.Common.Interfaces;

public interface IUser
{
    long Id { get; }
    UserRole Role { get; }
}
