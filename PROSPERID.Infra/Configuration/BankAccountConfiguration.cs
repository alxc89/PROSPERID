using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Core.Entities;

namespace PROSPERID.Infra.Configuration;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        #region table name
        builder.ToTable(nameof(BankAccount));
        #endregion

        #region primary key
        builder.HasKey(b => b.Id);
        #endregion

        #region index
        builder.HasIndex(b => b.AccountNumber)
            .IsUnique();
        #endregion

        #region mapping properties
        builder.Property(b => b.AccountNumber)
            .HasColumnName("AccountNumber")
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(c => c.Transactions)
             .WithOne(c => c.BankAccount)
             .OnDelete(DeleteBehavior.SetNull)
             .HasForeignKey(c => c.BankAccountId);

        builder.Property(b => b.AccountHolder)
            .HasColumnName("AccountHolder")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(b => b.Balance)
            .HasColumnName("Balance")
            .HasColumnType("decimal(10,2)")
            .IsRequired();
        #endregion

        #region check constraint
        builder.ToTable(t => t.HasCheckConstraint("CK_Balance", "Balance >= 0"));
        #endregion
    }
}
