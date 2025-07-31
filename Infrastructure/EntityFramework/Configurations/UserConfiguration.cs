using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftBank.Infrastructure.Entities;
namespace SofBank.Infrastructure.EntityFramework.Repositories;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.
          HasMany(u => u.Cards)
         .WithOne(c => c.User);

        builder.
         HasOne(s => s.Transactions)
         .WithOne(t => t.Sender);

        builder.
          HasOne(r => r.Transactions)
         .WithOne(t => t.Recipient);
    }
}