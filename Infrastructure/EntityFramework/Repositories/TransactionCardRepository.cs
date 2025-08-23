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

public class TransactionCardRepository : ITransactionCardRepository
{
    private readonly SoftBankDbContext _context;

    public TransactionCardRepository(SoftBankDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionCardDto>> GetAllAsync()
    {
        var actransaction = await _context.transactionCards.ToListAsync();
        return actransaction.Select(MapToDto);
    }

    public async Task<TransactionCardDto?> GetByIdAsync(Guid actransactionId)
    {
        var actransaction = await _context.transactionCards.FindAsync(actransactionId);
        return actransaction == null ? null : MapToDto(actransaction);
    }

    public async Task<Guid> CreateAsync(TransactionCardDto actransaction)
    {
        var actransactionEntity = MapToEntity(actransaction);
        actransactionEntity.Id = Guid.NewGuid();

        await _context.transactionCards.AddAsync(actransactionEntity);
        await _context.SaveChangesAsync();

        return actransactionEntity.Id;
    }

    public async Task<bool> UpdateAsync(TransactionCardDto cardDto)
    {
        var actransaction = await _context.transactionCards.FindAsync(cardDto.Id);
        if (actransaction == null)
            return false;

        // Update function
        _context.transactionCards.Update(MapToEntity(cardDto));

        // Save changes
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAsync(Guid actransactionId)
    {
        var actransaction = await _context.transactionCards.FindAsync(actransactionId);
        if (actransaction == null)
            return false;

        _context.transactionCards.Remove(actransaction);
        await _context.SaveChangesAsync();
        return true;
    }

    private TransactionCardDto MapToDto(TransactionCard actransaction)
    {
        return new TransactionCardDto
        {
            Id = actransaction.Id,
            CommitmentTransaction = actransaction.CommitmentTransaction,
            Amount = actransaction.Amount,
            Description = actransaction.Description,
            CardNumberRecipient = actransaction.CardNumberRecipient,
            CardNumberSender = actransaction.CardNumberSender,
            TrType = actransaction.TrType,
            TrStatus = actransaction.TrStatus,
            CurrencyType = actransaction.CurrencyType
        };
    }

    private TransactionCard MapToEntity(TransactionCardDto actransaction)
    {
        return new TransactionCard
        {
            Id = actransaction.Id,
            CommitmentTransaction = actransaction.CommitmentTransaction,
            Amount = actransaction.Amount,
            Description = actransaction.Description,
            CardNumberRecipient = actransaction.CardNumberRecipient,
            CardNumberSender = actransaction.CardNumberSender,
            TrType = actransaction.TrType,
            TrStatus = actransaction.TrStatus,
            CurrencyType = actransaction.CurrencyType
        };
    }
}
