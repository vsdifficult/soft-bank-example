using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
<<<<<<< HEAD

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
=======
using SoftBank.Infrastructure.Entities;

namespace SofBank.Infrastructure.EntityFramework.Repositories
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(t => t.Id);


            builder.HasOne(t => t.Sender)
                .WithMany(u => u.SentTransactions) 
                .HasForeignKey(t => t.SenderId);


            builder.HasOne(t => t.Recipient)
                .WithMany(u => u.ReceivedTransactions)
                .HasForeignKey(t => t.RecipientId);


            builder.HasOne(t => t.Card)
                .WithMany(c => c.Transaction)
                .HasForeignKey(t => t.FromCardId); 
        }
    }
}
>>>>>>> ffd8a5c1a5f383a6744c72f7c46bc3b739170361
