using Microsoft.EntityFrameworkCore;
using ApplicationHandler.Domain.Entities;

namespace ApplicationHandler.Infrastructure.Auth.Common.Interfaces;

public interface IAuthDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
