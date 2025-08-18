using SoftBank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace SofBank.Infrastructure.EntityFramework.Repositories;

public class CardConfiguration : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(EntityTypeBuilder<CardEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.
        HasOne(c => c.User)
        .WithMany(u => u.Cards)
        .HasForeignKey(c => c.UserId);
    }
}
// Bye Vlad =)