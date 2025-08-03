using Microsoft.EntityFrameworkCore;
using SofBank.Infrastructure.EntityFramework.Repositories;
using SoftBank.Infrastructure.Entities;
namespace SoftBank.Infrastructure.EntityFramework;

public class SoftBankDbContext : DbContext
{
    public SoftBankDbContext(DbContextOptions<SoftBankDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

    }
}