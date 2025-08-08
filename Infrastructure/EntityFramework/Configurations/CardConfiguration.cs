<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
=======
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
>>>>>>> ffd8a5c1a5f383a6744c72f7c46bc3b739170361
