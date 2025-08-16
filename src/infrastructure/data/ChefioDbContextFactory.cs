using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Chefio.Infrastructure.Data
{
    public class ChefioDbContextFactory : IDesignTimeDbContextFactory<ChefioDbContext>
    {
        public ChefioDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "api"));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ChefioDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34)));

            return new ChefioDbContext(optionsBuilder.Options);
        }
    }
}
