using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.
            HasMany(u => u.Cards)
            .WithOne(c => c.User);

        builder.
            HasOne(u => u.Transaction)
            .WithOne(t => t.User);
    }
}