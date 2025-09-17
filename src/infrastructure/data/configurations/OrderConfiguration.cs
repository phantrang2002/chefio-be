using Chefio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chefio.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(o => o.EmployeeId)
                .HasColumnName("employee_id")
                .IsRequired();

            builder.Property(o => o.TableId)
                .HasColumnName("table_id")
                .IsRequired();

            builder.Property(o => o.TimeIn)
                .HasColumnName("time_in")
                .IsRequired();

            builder.Property(o => o.TimeOut)
                .HasColumnName("time_out");

            builder.Property(o => o.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.HasOne<Employee>().WithMany().HasForeignKey(o => o.EmployeeId);
            builder.HasOne<Table>().WithMany().HasForeignKey(o => o.TableId);
        }
    }
}