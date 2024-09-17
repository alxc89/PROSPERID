using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Core.Entities;


namespace PROSPERID.Infra.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        #region table name
        builder.ToTable(nameof(Category));
        #endregion

        #region primary key
        builder.HasKey(c => c.Id);
        #endregion

        #region index
        builder.HasIndex(c => new { c.Id, c.Name });
        #endregion

        #region mapping properties
        builder.Property(c => c.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired(true);

        builder.HasMany(c => c.Transactions)
             .WithOne(c => c.Category)
             .HasForeignKey(c => c.CategoryId);
        #endregion
    }
}
