using Chefio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chefio.Infrastructure.Data.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("dishes");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(d => d.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            builder.Property(d => d.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.description)
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.Property(d => d.photo)
                .HasColumnName("photo")
                .HasMaxLength(255);

            builder.Property(d => d.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(d => d.isAvailable)
                .HasColumnName("is_available")
                .IsRequired();
 
            builder.HasOne<Category>().WithMany().HasForeignKey(d => d.CategoryId);
        }
    }
}