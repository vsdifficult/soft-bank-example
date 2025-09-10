using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftBank.Infrastructure.Entities;

namespace SofBank.Infrastructure.EntityFramework.Repositories;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
       public void Configure(EntityTypeBuilder<UserEntity> builder)
       {
              builder.HasKey(u => u.Id);
              builder.Property(u => u.FirstName);
              builder.Property(u => u.LastName);
              builder.Property(u => u.Email);
              builder.Property(u => u.Login);
              builder.Property(u => u.Password);
              builder.Property(u => u.DateOfBirth);
              builder.Property(u => u.UserRole);
              builder.Property(u => u.Code);

              builder.HasMany(u => u.Cards)
                     .WithOne()
                     .HasForeignKey(c => c.UserId);

                            builder.HasMany(u => u.SentTransactions)
                     .WithOne(t => t.Sender)
                     .HasForeignKey(t => t.SenderId)
                     .OnDelete(DeleteBehavior.Restrict);

              builder.HasMany(u => u.ReceivedTransactions)
                     .WithOne(t => t.Recipient)
                     .HasForeignKey(t => t.RecipientId)
                     .OnDelete(DeleteBehavior.Restrict);
       }
}