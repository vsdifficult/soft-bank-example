using SoftBank.Shared.Model; 
namespace SoftBank.Shared.Dto;

public record AccountDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public List<CardEntity> Cards { get; init; }

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

public record TransactionAccountDto
{
    public DateTime CommitmentTransaction { get; init; }

    public decimal Amount { get; init; }

    public string Description { get; init; }

    public Guid? AccountNumberRecipient { get; init; }

    public Guid? AccountNumberSender { get; init; }

    public Guid? UserSender { get; init; }

    public Guid? UserRecipient { get; init; }

    public TransactionType TrType { get; init; }

    public TransactionStatus TrStatus { get; init; }

    public Currency CurrencyType { get; init; }
}