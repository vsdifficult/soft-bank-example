
namespace SoftBank.Infrastructure.Entities;
public class CardEntity : BaseEntity
{
    public string CardNumber { get; set; } = string.Empty;

    public string CardHolderName { get; set; } = string.Empty;

    public DateTime ExpirationDate { get; set; }

    public string CVV { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public List<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>(); 

    public UserEntity? User { get; set; }
}