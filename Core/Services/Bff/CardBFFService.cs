using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using SoftBank.Core.Services.Interfaces;

namespace SoftBank.Core.Services.BFF;

// CardBFFService 
public class CardBFFService : ICardBFFService
{
    private readonly IDataService _dataService;

    public CardBFFService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<TransactionCardDto> ProcessPayment(CardPaymentDto payment)
    {
        var cardSender = await _dataService.cardRepository.GetByIdAsync(payment.CardNumberSender);

        if (cardSender == null)
        {
            throw new Exception("cardSender not found.");
        }

        var cardRecipient = await _dataService.cardRepository.GetByIdAsync(payment.CardNumberRecipient);

        if (cardRecipient == null)
        {
            throw new Exception("cardRecipient not found.");
        }

        if (cardSender.CurrencyType != cardRecipient.CurrencyType)
        {
            throw new Exception("CurrencyType is not available.");
        }

        var transactionCard = new TransactionCardDto
        {
            Id = Guid.NewGuid(),
            CommitmentTransaction = DateTime.Now,
            Amount = payment.Amount,
            TrType = TransactionType.Transfer,
            TrStatus = TransactionStatus.Pending,
            CurrencyType = payment.CurrencyType,
            CardNumberSender = payment.CardNumberSender,
            CardNumberRecipient = payment.CardNumberRecipient
        };

        // Updating Users Balances
        await _dataService.userRepository.UpdateUserBalance(cardSender.Id, cardSender.Amount - payment.Amount);
        await _dataService.userRepository.UpdateUserBalance(cardRecipient.Id, cardRecipient.Amount + payment.Amount);

        await _dataService.transactionCardRepository.CreateAsync(transactionCard);

        return transactionCard;
    }


    public async Task<CardStatisticsDto> GetStatistics(Guid cardId)
    {
        // Return Stat
        return await _dataService.cardRepository.GetCardStatistics(cardId);
    }
}
