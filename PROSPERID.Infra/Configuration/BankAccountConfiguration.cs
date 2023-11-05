using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PROSPERID.Domain.Entities;

namespace PROSPERID.Infra.Configuration;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable(nameof(BankAccount));
        builder.HasKey(b => new { b.Id, b.AccountNumber });
        builder.Property(b => b.AccountNumber)
            .HasColumnType("varchar(20)")
            .IsRequired();
        builder.Property(b => b.AccountHolder)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(b => b.Balance)
            .HasColumnType("decimal(10,2)")
            .IsRequired();
    }
}
