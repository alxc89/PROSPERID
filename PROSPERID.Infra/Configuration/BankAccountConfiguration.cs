using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Core.Entities;

namespace PROSPERID.Infra.Configuration;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable(nameof(BankAccount));

        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.AccountNumber)
            .IsUnique();

        builder.Property(b => b.AccountNumber)
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(c => c.Transactions)
             .WithOne(c => c.BankAccount)
             .OnDelete(DeleteBehavior.SetNull)
             .HasForeignKey(c => c.BankAccountId);

        builder.Property(b => b.AccountHolder)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(b => b.Balance)
            .HasColumnType("decimal(10,2)")
            .IsRequired();
    }
}
