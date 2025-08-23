using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using SoftBank.Core.Services.Interfaces;
using SoftBank.Core.Repositories;

namespace SoftBank.Core.Services.BFF;

// CardBFFService 
public class CardBFFService : ICardBFFService
{
    private readonly ITransactionCardRepository _cardTransactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;

    public CardBFFService(ITransactionCardRepository cardTransactionRepository, IUserRepository userRepository, ICardRepository cardRepository)
    {
        _cardTransactionRepository = cardTransactionRepository;
        _userRepository = userRepository;
        _cardRepository = cardRepository;
    }

    public async Task<TransactionCardDto> ProcessPayment(CardPaymentDto payment)
    {
        var cardSender = await _cardTransactionRepository.GetByIdAsync(payment.CardNumberSender);

        if (cardSender == null)
        {
            throw new Exception("cardSender not found.");
        }

        var cardRecipient = await _cardTransactionRepository.GetByIdAsync(payment.CardNumberRecipient);

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
        await _userRepository.UpdateUserBalance(cardSender.Id, cardSender.Amount - payment.Amount);
        await _userRepository.UpdateUserBalance(cardRecipient.Id, cardRecipient.Amount + payment.Amount);

        await _cardTransactionRepository.CreateAsync(transactionCard);

        return transactionCard;
    }


    public async Task<CardStatisticsDto> GetStatistics(Guid cardId)
    {
        // Retuurn Stat
        return await _cardRepository.GetCardStatistics(cardId);
    }
}
