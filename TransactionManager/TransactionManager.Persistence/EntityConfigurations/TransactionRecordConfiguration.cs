using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionManager.Domain;

namespace TransactionManager.Persistence.EntityConfigurations;

public class TransactionRecordConfiguration : IEntityTypeConfiguration<TransactionRecord>
{
    public void Configure(EntityTypeBuilder<TransactionRecord> builder)
    {
        builder.HasKey(x => x.TransactionRecordId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.TransactionDate).IsRequired();

        builder.Property(x => x.ClientLocation)
            .IsRequired()
            .HasMaxLength(50);
    }
}