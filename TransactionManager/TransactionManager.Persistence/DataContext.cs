using Microsoft.EntityFrameworkCore;
using TransactionManager.Domain;
using TransactionManager.Persistence.EntityConfigurations;

namespace TransactionManager.Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<TransactionRecord> TransactionRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TransactionRecordConfiguration());
    }
}