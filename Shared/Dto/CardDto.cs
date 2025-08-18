namespace SoftBank.Shared.Dto;

public record CardDto 
{
    public Guid  Id { get; init; }

    public Guid UserId { get; init; }

    public Guid AccountId { get; init; }

    public string CardNumber { get; init; }

    public string CardHolderName { get; init; }

    public DateTime ExpirationDate { get; init; }

    public string CVV { get; init; }

    public List<TransactionEntity> Transactions { get; init; } = new List<TransactionEntity>();
}
