using SoftBank.Shared.Model;

namespace SoftBank.Shared.Dto;

public record ClientHistoryPayments
{
    public string Type { get; init; }

    public string userIdSender { get; init; }

    public string userIdRecipient { get; init; }

    public decimal Amount { get; init; }

    public DateTime Date { get; init; }

    public Guid TransactionId { get; init; }
}
