using Microsoft.EntityFrameworkCore;
using SoftBank.Infrastructure.Entities;
namespace SoftBank.Infrastructure.EntityFramework;

public class SoftBankDbContext : DbContext
{
    public SoftBankDbContext()
    {

    }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }

}