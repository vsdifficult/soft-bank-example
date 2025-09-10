using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftBank.Infrastructure.Entities;

namespace SofBank.Infrastructure.EntityFramework.Repositories
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CommitmentTransaction);
            builder.Property(t => t.Amount);
            builder.Property(t => t.Description);
            builder.Property(t => t.SenderId);
            builder.Property(t => t.RecipientId);
            builder.Property(t => t.FromCardId);
            builder.Property(t => t.ToCardId);
            builder.Property(t => t.TrType);
            builder.Property(t => t.TrStatus);

            builder.HasOne(t => t.Sender)
                   .WithMany()
                   .HasForeignKey(t => t.SenderId)
                   .IsRequired(false); // Assuming SenderId can be null

            builder.HasOne(t => t.Recipient)
                   .WithMany()
                   .HasForeignKey(t => t.RecipientId)
                   .IsRequired(false); // Assuming RecipientId can be null
        }
    }
}
