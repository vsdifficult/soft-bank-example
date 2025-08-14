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