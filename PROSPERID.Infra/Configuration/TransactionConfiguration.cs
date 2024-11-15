using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Infra.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        #region table name
        builder.ToTable(nameof(Transaction));
        #endregion

        #region primary key
        builder.HasKey(t => t.Id);
        #endregion

        #region mapping properties
        builder.Property(t => t.Description)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        //Fazer o mapping do enum
        builder.Property(t => t.Type)
            .IsRequired()
            .HasColumnType("SMALLINT");

        //fazer o check desse e de outras propriedades
        builder.OwnsOne(t => t.Amount, a =>
        {
            a.Property(m => m.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            a.Property(m => m.Amount)
                .HasColumnName("Amount")
                .HasMaxLength(3)
                .IsRequired(); // Define um tamanho máximo e exige a presença do valor
        });

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

        builder.HasOne(t => t.PaymentMethod)
            .WithMany(t => t.Transactions)
            .HasForeignKey(t => t.PaymentMethodId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.CategoryId);
        #endregion
    }
}
