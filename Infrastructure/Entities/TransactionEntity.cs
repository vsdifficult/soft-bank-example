using SoftBank.Shared.Model;

namespace SoftBank.Infrastructure.Entities;

public class TransactionEntity : BaseEntity
{
    public DateTime CommitmentTransaction { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid? SenderId { get; set; }
    public UserEntity? Sender { get; set; }

    public Guid? RecipientId { get; set; }
    public UserEntity? Recipient { get; set; }
    public Guid FromCardId { get; set; }

    public Guid ToCardId { get; set; }

    public CardEntity? Card { get; set; }

    public TransactionType TrType { get; set; }
    public TransactionStatus TrStatus { get; set; }
    
}

public class TransactionAccount : BaseEntity
{
    public DateTime CommitmentTransaction { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid? AccountNumberRecipient { get; set; }

    public Guid? AccountNumberSender { get; set; }

    public TransactionType TrType { get; set; }

    public TransactionStatus TrStatus { get; set; }

    public Currency CurrencyType { get; set; }
}

public class TransactionCard : BaseEntity
{
    public DateTime CommitmentTransaction { get; init; }

    public decimal Amount { get; init; }

    public string Description { get; init; }

    public Guid? CardNumberRecipient { get; init; }

    public Guid? CardNumberSender { get; init; }

    public TransactionType TrType { get; init; }

    public TransactionStatus TrStatus { get; init; }

    public Currency CurrencyType { get; init; }
}
