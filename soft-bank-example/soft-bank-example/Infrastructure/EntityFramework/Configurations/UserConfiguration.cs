using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace SoftBank.Infrastructure.Entitites;
public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
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
    }
}