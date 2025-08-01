using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftBank.Infrastructure.Entities;

namespace SofBank.Infrastructure.EntityFramework.Repositories;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.Cards)
               .WithOne(c => c.User);

        builder.HasMany(u => u.SentTransactions)
               .WithOne(t => t.Sender); // Указываем, что TransactionEntity имеет свойство Sender

        // Связь с полученными транзакциями
        builder.HasMany(u => u.ReceivedTransactions)
               .WithOne(t => t.Recipient); // Указываем, что TransactionEntity имеет свойство Recipient
    }
}