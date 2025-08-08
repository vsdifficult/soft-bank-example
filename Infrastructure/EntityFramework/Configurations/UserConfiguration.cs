using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
<<<<<<< HEAD
=======
using SoftBank.Infrastructure.Entities;

namespace SofBank.Infrastructure.EntityFramework.Repositories;
>>>>>>> ffd8a5c1a5f383a6744c72f7c46bc3b739170361

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
<<<<<<< HEAD
        builder.HasKey(s => s.Id);
        builder.HasKey(r => r.Id);

        builder.
            HasMany(u => u.Cards)
            .WithOne(c => c.User);

        builder.
            HasOne(s => s.Transaction)
            .WithOne(t => t.Sender);

        builder.
            HasOne(r => r.Transaction)
            .WithOne(t => t.Recipient);
=======

        builder.HasMany(u => u.Cards)
               .WithOne(c => c.User);

        builder.HasMany(u => u.SentTransactions)
               .WithOne(t => t.Sender); // Указываем, что TransactionEntity имеет свойство Sender

        // Связь с полученными транзакциями
        builder.HasMany(u => u.ReceivedTransactions)
               .WithOne(t => t.Recipient); // Указываем, что TransactionEntity имеет свойство Recipient
>>>>>>> ffd8a5c1a5f383a6744c72f7c46bc3b739170361
    }
}