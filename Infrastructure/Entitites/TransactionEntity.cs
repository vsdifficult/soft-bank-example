using SoftBank.Shared.Model;

namespace SoftBank.Infrastructure.Entitites;
public class TransactionEntity : BaseEntity
{
    public DateTime CommitmentTransaction { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid? SenderId { get; set; }
    // public required UserEntity Sender { get; set; }
    public Guid? RecipientId { get; set; }
    // public required UserEntity Recipient { get; set; }

    public Guid FromCardId { get; set; }

    public Guid ToCardId { get; set; }

    public CardEntity? Card { get; set; }

    public TransactionType TrType { get; set; }
    public TransactionStatus TrStatus { get; set; }
}