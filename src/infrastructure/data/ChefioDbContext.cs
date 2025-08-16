using Chefio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chefio.Infrastructure.Data;

public class ChefioDbContext : DbContext
{
    public ChefioDbContext(DbContextOptions<ChefioDbContext> options) : base(options) {}

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Account> Accounts { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChefioDbContext).Assembly);
    }
}
