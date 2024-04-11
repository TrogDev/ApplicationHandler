namespace ApplicationHandler.Infrastructure.Data;

using System.Reflection;
using Microsoft.EntityFrameworkCore;

using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Infrastructure.Auth.Common.Interfaces;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IAuthDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Keyword> Keywords { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
