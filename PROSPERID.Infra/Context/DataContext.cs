using Microsoft.EntityFrameworkCore;
using PROSPERID.Domain.Entities;
using PROSPERID.Infra.Configuration;

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
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }
}
