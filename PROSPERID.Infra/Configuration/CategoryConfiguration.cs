using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Domain.Entities;

namespace PROSPERID.Infra.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category));

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired(true);

        builder.HasMany(c => c.Transactions)
             .WithOne(c => c.Category)
             .HasForeignKey(c => c.Id);
    }
}
