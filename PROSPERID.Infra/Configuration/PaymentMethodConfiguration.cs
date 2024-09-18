using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Infra.Configuration;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        #region table name
        builder.ToTable(nameof(PaymentMethod));
        #endregion

        #region primary key
        builder.HasKey(p => p.Id);
        #endregion

        #region mapping properties
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(p => p.PaymentMethodType)
            .HasColumnName("PaymentMethodType")
            .HasColumnType("varchar(15)")
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (EPaymentMethodType)Enum.Parse(typeof(EPaymentMethodType), v)
            );

        builder.Property(p => p.IsActive)
            .HasColumnName("IsActive")
            .HasColumnType("bit")
            .HasDefaultValue(true);

        builder.HasOne(p => p.BankAccount)
            .WithOne(p => p.PaymentMethod)
            .HasForeignKey<PaymentMethod>(p => p.BankAccountId)
            .IsRequired(false);

        builder.HasOne(p => p.CreditCard)
            .WithOne(p => p.PaymentMethod)
            .HasForeignKey<PaymentMethod>(p => p.CreditCardId)
            .IsRequired(false);

        builder.HasMany(p => p.Transactions)
            .WithOne(t => t.PaymentMethod)
            .HasForeignKey(t => t.PaymentMethodId)
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region check constraint
        builder.ToTable(c => c.HasCheckConstraint("CK_PaymentMethodType",
            $"PaymentMethodType in ('{nameof(EPaymentMethodType.BankAccount)}', '{nameof(EPaymentMethodType.CreditCard)}', '{nameof(EPaymentMethodType.Other)}')"
            ));

        builder.ToTable(c => c.HasCheckConstraint("CK_PaymentMethodIsActive",
            $"IsActive in (0, 1)"
            ));
        #endregion
    }
}