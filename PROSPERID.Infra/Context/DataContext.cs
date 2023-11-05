using Microsoft.EntityFrameworkCore;
using PROSPERID.Domain.Entities;
using PROSPERID.Infra.Configuration;
using PROSPERID.Infra.ExtensionsMethods;

namespace PROSPERID.Infra.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ConfigureOwnedTypeNavigationsAsRequired();
    }
}
