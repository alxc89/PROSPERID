using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Infra.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction));

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Description)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Type)
            .IsRequired()
            .HasColumnType("SMALLINT");

        builder.Property(t => t.Amount)
            .HasColumnType("decimal(10, 2)")
            .IsRequired();

        builder.Property(t => t.TransactionDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(t => t.DueDate)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(t => t.PaymentDate)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(t => t.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.HasOne(t => t.BankAccount)
            .WithMany(b => b.Transactions)
            .HasForeignKey(t => t.BankAccountId)
            .OnDelete(DeleteBehavior.SetNull);
    
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.CategoryId);
    }
}
