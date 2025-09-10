using SoftBank.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace SofBank.Infrastructure.EntityFramework.Repositories;

public class CardConfiguration : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(EntityTypeBuilder<CardEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.AccountId);
        builder.Property(c => c.UserId);
        builder.Property(c => c.CardNumber);
        builder.Property(c => c.CardHolderName);
        builder.Property(c => c.ExpirationDate);
        builder.Property(c => c.CVV);
    }
}
// Bye Vlad =)