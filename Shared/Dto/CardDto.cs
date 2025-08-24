using SoftBank.Shared.Model;

namespace SoftBank.Shared.Dto;

public record CardDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid AccountId { get; init; }

    public string CardNumber { get; init; }

    public string CardHolderName { get; init; }

    public DateTime ExpirationDate { get; init; } 
    public Currency CurrencyType { get; init; }
    public decimal Amount { get; init; }

    public string CVV { get; init; }

    public List<Guid> Transactions { get; init; } = new List<Guid>();
}

public record CardPaymentDto
{
    public Guid CardNumberSender { get; init; }

    public Guid CardNumberRecipient { get; init; }

    public decimal Amount { get; init; }

    public Currency CurrencyType { get; init; }
}

public record TransactionCardDto
{
    public Guid Id { get; init; }

    public DateTime CommitmentTransaction { get; init; }

    public decimal Amount { get; init; }

    public string Description { get; init; } = string.Empty;

    public Guid? CardNumberRecipient { get; init; }

    public Guid? CardNumberSender { get; init; }

    public TransactionType TrType { get; init; }

    public TransactionStatus TrStatus { get; init; }

    public Currency CurrencyType { get; init; }
}

public record CardStatisticsDto
{
    public Guid CardId { get; init; }

    public DateTime CreatedAt { get; init; }

    public int TransationsQuantity { get; init; }

    public decimal SpendAmount { get; init; }

    public decimal EarnAmount { get; init; }

    public List<TransactionCardDto> TransactionsHistory { get; init; } = new List<TransactionCardDto>();
}
