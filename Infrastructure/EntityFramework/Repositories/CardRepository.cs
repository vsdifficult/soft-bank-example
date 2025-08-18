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

public class CardRepository : IAccountRepository
{
    private readonly SoftBankDbContext _context;

    public CardRepository(SoftBankDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CardDto>> GetAllAsync()
    {
        var card = await _context.Cards.ToListAsync();
        return cards.Select(MapToDto);
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
