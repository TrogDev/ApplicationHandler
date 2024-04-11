namespace ApplicationHandler.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;
using ApplicationHandler.Domain.Entities;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Application> Applications { get; set; }
    DbSet<Keyword> Keywords { get; set; }
    DbSet<Department> Departments { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}