using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.
            HasOne(t => t.Sender)
            .WithOne(s => s.Transaction);

        builder.
            HasOne(t => t.Card)
            .WithOne(c => c.Transaction);

        builder.HasOne(t => t.Recipient)
            .WithMany()
            .HasForeignKey(t => t.RecipientId);
    }
}