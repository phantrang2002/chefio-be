using Chefio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chefio.Infrastructure.Data;

public class ChefioDbContext : DbContext
{
    public ChefioDbContext(DbContextOptions<ChefioDbContext> options) : base(options) {}

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChefioDbContext).Assembly);
    }
}
