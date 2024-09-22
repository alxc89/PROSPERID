using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Core.Entities;

namespace PROSPERID.Infra.Configuration;

public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
{
    public void Configure(EntityTypeBuilder<CreditCard> builder)
    {
        #region table name
        builder.ToTable(nameof(CreditCard));
        #endregion

        #region primary key
        builder.HasKey(c => c.Id);
        #endregion

        #region mapping properties
        builder.OwnsOne(c => c.Number, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName("Number")
                .HasColumnType("varchar(5)")
                .IsRequired();
        });

        builder.Property(c => c.HolderName)
            .HasColumnName("HolderName")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.ExpirationDate)
            .HasColumnName("ExpirationDate")
            .HasColumnType("date");

        builder.OwnsOne(c => c.CreditLimit, cl =>
        {
            cl.Property(lm => lm.Amount)
                .HasColumnName("CreditLimit")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0.00)
                .IsRequired();
        });

        builder.OwnsOne(c => c.CurrentBalance, cb =>
        {
            cb.Property(m => m.Amount)
                .HasColumnName("CurrentBalance")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0.00);
        });

        builder.Property(c => c.DueDate)
            .HasColumnName("DueDate")
            .HasColumnType("date")
            .IsRequired();

        builder.HasMany(c => c.CreditCardBill)
            .WithOne(c => c.CreditCard)
            .HasForeignKey(c => c.CreditCardId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(c => c.PaymentMethod)
            .WithOne(c => c.CreditCard)
            .HasForeignKey<PaymentMethod>(c => c.CreditCardId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
        #endregion

        #region check constraint
        builder.ToTable(t => t.HasCheckConstraint("CK_CreditLimit", "CreditLimit >= 0"));
        builder.ToTable(t => t.HasCheckConstraint("CK_CurrentBalance", "CurrentBalance >= 0"));
        #endregion
    }
}