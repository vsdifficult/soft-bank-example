using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
namespace SoftBank.Core.Services.Interfaces;


public interface ICardBFFService
{
    Task<TransactionCardDto> ProcessPayment(CardPaymentDto payment);

    Task<CardStatisticsDto> GetStatistics(Guid cardId);
}
