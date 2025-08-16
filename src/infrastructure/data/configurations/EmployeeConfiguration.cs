using Chefio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chefio.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Address)
            .HasColumnName("address")
            .HasMaxLength(100);

        builder.Property(e => e.Note)
            .HasColumnName("note");

        builder.Property(e => e.AccountId)
            .HasColumnName("account_id")
            .IsRequired();

        builder.HasOne<Account>()
            .WithOne()
            .HasForeignKey<Employee>(e => e.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}