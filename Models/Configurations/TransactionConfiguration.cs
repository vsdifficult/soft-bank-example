using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.
            HasOne(t => t.User)
            .WithOne(u => u.Transaction);

        builder.
            HasOne(t => t.Card)
            .WithOne(c => c.Transaction);
    }
}