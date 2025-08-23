using Microsoft.EntityFrameworkCore;
using SoftBank.Core.Repositories;
using SoftBank.Infrastructure.EntityFramework;
using SoftBank.Infrastructure.Entities;
using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SoftBank.Infrastructure.EntityFramework.Repositories;

public class CardRepository : ICardRepository
{
    private readonly SoftBankDbContext _context;

    public CardRepository(SoftBankDbContext context)
    {
        _context = context;
    }
    public async Task<CardDto> GetByUserIdAsync(Guid userId)
    {
        var card = await _context.Cards.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);
        return card == null ? null : MapToDto(card);
    }
    public async Task<CardDto?> GetByAccountIdAsync(Guid accountId)
    {
        var card = await _context.Cards.AsNoTracking().FirstOrDefaultAsync(c => c.AccountId == accountId);
        return card == null ? null : MapToDto(card);
    }
    public async Task<CardDto> GetByCvvAsync(string cvv)
    {
        var card = await _context.Cards.AsNoTracking().FirstOrDefaultAsync(c => c.CVV == cvv);
        return card == null ? null : MapToDto(card);
    }
    public async Task<IEnumerable<CardDto>> GetAllAsync()
    {
        var card = await _context.Cards.AsNoTracking().ToListAsync();
        return card.Select(MapToDto);
    }
    public async Task<CardDto> GetByCardNumberAsync(string cardNumber)
    {
        var card = await _context.Cards.AsNoTracking().FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        return card == null ? null : MapToDto(card);
    }

    public async Task<CardDto?> GetByIdAsync(Guid cardId)
    {
        var card = await _context.Cards.FindAsync(cardId);
        return card == null ? null : MapToDto(card);
    }

    public async Task<Guid> CreateAsync(CardDto card)
    {
        var cardEntity = MapToEntity(card);
        cardEntity.Id = Guid.NewGuid(); 

        await _context.Cards.AddAsync(cardEntity);
        await _context.SaveChangesAsync();

        return cardEntity.Id;
    }

    public async Task<bool> DeleteAsync(Guid cardId)
    {
        var card = await _context.Cards.FindAsync(cardId);
        if (card == null)
            return false;

        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateAsync(CardDto card)
    {
        var cardEntity = await _context.Cards.FindAsync(card.Id);
        if (cardEntity == null) return false;

        _context.Cards.Update(MapToEntity(card));
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<CardStatisticsDto> GetCardStatistics(Guid cardId)
    {


        // Creating variables with LINQ references
        var createdAt = DateTime.Now;
        var transationsQuantity = _context.transactionCards.Count();
        var spendAmount = _context.transactionCards.Where(t => t.Amount < 0).Sum(t => (decimal?)t.Amount * -1) ?? 0m;
        var earnAmount = _context.transactionCards.Where(t => t.Amount > 0).Sum(t => (decimal?)t.Amount) ?? 0m;
        var transactionsHistory = _context.transactionCards
                    .Select(t => new TransactionCardDto
                    {
                        Id = t.Id,
                        CommitmentTransaction = t.CommitmentTransaction,
                        Amount = t.Amount,
                        Description = t.Description,
                        CardNumberRecipient = t.CardNumberRecipient,
                        CardNumberSender = t.CardNumberSender,
                        TrType = t.TrType,
                        TrStatus = t.TrStatus,
                        CurrencyType = t.CurrencyType
                    }).ToList();

        // Return of the completed AccountStatisticsDto
        return new CardStatisticsDto
        {
            CardId = cardId,
            CreatedAt = createdAt,
            TransationsQuantity = transationsQuantity,
            SpendAmount = spendAmount,
            EarnAmount = earnAmount,
            TransactionsHistory = transactionsHistory
        };
    }

    private CardDto MapToDto(CardEntity card)
    {
        return new CardDto
        {
            Id = card.Id,
            CardNumber = card.CardNumber,
            CardHolderName = card.CardHolderName,
            ExpirationDate = card.ExpirationDate,
            CVV = card.CVV,
            UserId = card.UserId,
            Transactions = card.Transactions,
            AccountId = card.AccountId
        };
    }

    private CardEntity MapToEntity(CardDto card)
    {
        return new CardEntity
        {
            Id = card.Id,
            CardNumber = card.CardNumber,
            CardHolderName = card.CardHolderName,
            ExpirationDate = card.ExpirationDate,
            CVV = card.CVV,
            UserId = card.UserId,
            Transactions = card.Transactions,
            AccountId = card.AccountId
        };
    }


}
