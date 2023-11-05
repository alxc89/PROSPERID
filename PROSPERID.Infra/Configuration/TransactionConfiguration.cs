using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Domain.Entities;
using PROSPERID.Domain.Enums;

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
            .HasColumnType("varchar")
            .HasMaxLength(5)
            .HasConversion(
                t => t.ToString(),
                t => (TransactionType)Enum.Parse(typeof(TransactionType), t))
            .IsRequired();
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
        builder.HasMany(t => t.BankAccounts)
            .WithMany(t => t.Transactions)
            .UsingEntity<Dictionary<string, object>>(
                "TransactionAccount",
                transaction => transaction
                    .HasOne<BankAccount>()
                    .WithMany()
                    .HasForeignKey("BankAccountId")
                    .HasConstraintName("FK_TransaciontAccount_BancAccountId")
                    .HasPrincipalKey(t => t.Id)
                    .OnDelete(DeleteBehavior.Cascade),
                bankAccount => bankAccount
                    .HasOne<Transaction>()
                    .WithMany()
                    .HasForeignKey("TransactionId")
                    .HasConstraintName("FK_TransaciontAccount_TransactionId")
                    .OnDelete(DeleteBehavior.Cascade)
            );

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.CategoryId);
    }
}
