using SoftBank.Shared.Dto;

namespace SoftBank.Core.Repositories;

public interface ICardRepository : IRepository<CardDto, Guid>
{
    Task<CardDto> GetByUserIdAsync(Guid userId);

    Task<CardDto> GetByCvvAsync(string cvv);

    Task<CardDto> GetByCardNumberAsync(string cardNumber);

    Task<CardStatisticsDto> GetCardStatistics(Guid cardId);
}
