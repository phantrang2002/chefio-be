using Chefio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chefio.Infrastructure.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Username).HasMaxLength(100).IsRequired();
        builder.Property(a => a.Password).HasMaxLength(100).IsRequired();
        builder.Property(a => a.Role).IsRequired();
        builder.Property(a => a.IsActive).IsRequired();
    }
}