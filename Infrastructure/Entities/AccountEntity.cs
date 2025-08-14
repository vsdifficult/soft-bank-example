namespace SoftBank.Infrastructure.Entities;

public class AccountEntity : BaseEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public List<CardEntity> Cards { get; set; }

    public Account AccountType { get; set; }

    public Currency CurrencyType { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}