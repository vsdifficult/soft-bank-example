using SoftBank.Shared.Model; 
namespace SoftBank.Shared.Dto;

public record AccountDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public List<Guid> Cards { get; init; } = new List<Guid>();

    public Account AccountType { get; init; }

    public Currency CurrencyType { get; init; }

    public decimal Amount { get; init; }

    public DateTime CreatedAt { get; init; }
    
}

public record PaymentDto
{
    public Guid AccountNumberSender { get; init; }

    public Guid AccountNumberRecipient { get; init; }

    public decimal Amount { get; init; }

    public Currency CurrencyType { get; init; }
}

public record TransferDto
{
    public Guid SenderId { get; init; }

    public Guid CardSenderId { get; init; }

    public Guid CardRecipientId { get; init; }

    public Guid AccountSenderId { get; init; }

    public Guid AccountRecipientId { get; init; }

    public Guid RecipientId { get; init; }

    public decimal Amount { get; init; }

    public DateTime TransactionDate { get; init; }

    public Currency CurrencyType { get; init; }

    public string Description { get; init; }

    public TransactionStatus TrStatus { get; init; }

    public TransferType TransferType { get; init; }
}

public record TransactionAccountDto
{
    public Guid Id { get; init; }

    public DateTime CommitmentTransaction { get; init; }

    public decimal Amount { get; init; }

    public string Description { get; init; }

    public Guid? AccountNumberRecipient { get; init; }

    public Guid? AccountNumberSender { get; init; }

    public TransactionType TrType { get; init; }

    public TransactionStatus TrStatus { get; init; }

    public Currency CurrencyType { get; init; }
}

public record AccountStatisticsDto
{
    public Guid AccountId { get; init; }

    public DateTime CreatedAt { get; init; }

    public int TransationsQuantity { get; init; }

    public decimal SpendAmount { get; init; }

    public decimal EarnAmount { get; init; }

    public List<TransactionAccountDto> TransactionsHistory { get; init; } = new List<TransactionAccountDto>();
}
