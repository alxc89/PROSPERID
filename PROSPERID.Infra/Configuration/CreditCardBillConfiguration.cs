using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Core.Entities;
using PROSPERID.Core.Enums;

namespace PROSPERID.Infra.Configuration;

public class CreditCardBillConfiguration : IEntityTypeConfiguration<CreditCardBill>
{
    public void Configure(EntityTypeBuilder<CreditCardBill> builder)
    {
        #region table name
        builder.ToTable(nameof(CreditCardBill));
        #endregion

        #region primary key
        builder.HasKey(c => c.Id);
        #endregion

        #region mapping properties
        builder.Property(c => c.BillDate)
            .HasColumnName("BillDate")
            .HasColumnType("datetime")
            .IsRequired(true);

        builder.Property(c => c.DueDate)
            .HasColumnName("DueDate")
            .HasColumnType("datetime")
            .IsRequired(true);

        builder.Property(c => c.BillingPeriod)
            .HasColumnName("BillingPeriod")
            .HasColumnType("datetime");

        builder.Property(c => c.CloseDate)
            .HasColumnName("CloseDate")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(c => c.Status)
            .HasColumnName("Status")
            .HasColumnType("varchar(10)")
            .HasConversion(
                v => v.ToString(),
                v => (EStatus)Enum.Parse(typeof(EStatus), v)
            );

        builder.OwnsOne(c => c.TotalAmount, ta =>
        {
            ta.Property(c => c.Amount)
                .HasColumnName("TotalAmount")
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0.00);
        });

        builder.OwnsOne(c => c.PaidAmount, pa =>
        {
            pa.Property(c => c.Amount)
                .HasColumnName("PaidAmount")
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0.00);
        });

        builder.Property(c => c.PaymentStatus)
            .HasColumnName("PaymentStatus")
            .HasColumnType("varchar(10)")
            .HasConversion(
                v => v.ToString(),
                v => (EPaymentStatus)Enum.Parse(typeof(EPaymentStatus), v)
            );

        builder.Property(c => c.CreditCardId)
            .HasColumnName("CreditCardId")
            .HasColumnType("bigint")
            .IsRequired(false);

        builder.HasOne(c => c.CreditCard)
            .WithMany(c => c.CreditCardBill)
            .HasForeignKey(c => c.CreditCardId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Transactions)
            .WithOne(c => c.CreditCardBill)
            .OnDelete(DeleteBehavior.SetNull)
            .HasForeignKey(c => c.CreditCardBillId);
        #endregion

        #region check constraint
        builder.ToTable(t => t.HasCheckConstraint("CK_PaymentStatus",
            $"PaymentStatus IN ('{nameof(EPaymentStatus.Paid)}', '{nameof(EPaymentStatus.Pending)}', '{nameof(EPaymentStatus.Overdue)}')"));

        builder.ToTable(t => t.HasCheckConstraint("CK_Status",
            $"Status IN ('{nameof(EStatus.Open)}', '{nameof(EStatus.Close)}')"));

        builder.ToTable(t => t.HasCheckConstraint("CK_TotalAmount", "TotalAmount >= 0"));

        builder.ToTable(t => t.HasCheckConstraint("CK_PaidAmount", "PaidAmount >= 0"));
        #endregion
    }
}
