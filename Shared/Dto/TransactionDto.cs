using SoftBank.Shared.Model; 

namespace SoftBank.Shared.Dto;

public record TransactionDto
{
    public Guid Id { get; init; }
    public Guid SenderId { get; init; }
    public Guid RecipientId { get; init; }
    public DateTime Date { get; init; } = DateTime.UtcNow; 
    public decimal Amount { get; init; }
    public TransactionStatus Status { get; init; }
    public Currency CurrencyType { get; init; }
    public TransferType Ttype { get; init; }
}