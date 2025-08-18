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

public class TransactionAccountRepository : ITransactionAccountsRepository
{   
    private readonly SoftBankDbContext _context;

    public TransactionAccountRepository(SoftBankDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionAccountDto>> GetAllAsync()
    {
        var actransaction = await _context.TransactionAccount.ToListAsync();
        return actransactions.Select(MapToDto);
    }

    public async Task<TransactionAccountDto?> GetByIdAsync(Guid actransactionId)
    {
        var actransaction = await _context.TransactionsAccount.FindAsync(actransactionId);
        return actransaction == null ? null : MapToDto(actransaction);
    }

    public async Task<Guid> CreateAsync(TransactionAccountDto actransaction)
    {
        var actransactionEntity = MapToEntity(actransaction);
        actransactionEntity.Id = Guid.NewGuid(); 

        await _context.TransactionAccount.AddAsync(TransactionAccountEntity);
        await _context.SaveChangesAsync();

        return TransactionAccountEntity.Id;
    }

    public async Task<bool> DeleteAsync(Guid actransactionId)
    {
        var actransaction = await _context.TransactionAccount.FindAsync(actransactionId);
        if (actransaction == null)
            return false;

        _context.TransactionAccount.Remove(actransaction);
        await _context.SaveChangesAsync();
        return true;
    }

    private TransactionAccountDto MapToDto(TransactionAccountEntity actransaction)
    {
        return new TransactionAccountDto
        {
            Id = actransaction.Id,
            CommitmentTransaction = actransaction.CommitmentTransaction,
            Amount = actransaction.Amount,
            Description = actransaction.Description;
            AccountNumberRecipient = actransaction.AccountNumberRecipient,
            AccountNumberSender = actransaction.AccountNumberSender,
            UserSender = actransaction.UserSender,
            UserRecipient = actransaction.UserRecipient,
            TrType = actransaction.TrType,
            TrStatus = actransaction.TrStatus,
            CurrencyType = actransaction.CurrencyType
        };
    }

    private TransactionAccountEntity MapToEntity(TransactionAccountDto actransaction)
    {
        return new TransactionAccountEntity
        {
            Id = actransaction.Id,
            CommitmentTransaction = actransaction.CommitmentTransaction,
            Amount = actransaction.Amount,
            Description = actransaction.Description;
            AccountNumberRecipient = actransaction.AccountNumberRecipient,
            AccountNumberSender = actransaction.AccountNumberSender,
            UserSender = actransaction.UserSender,
            UserRecipient = actransaction.UserRecipient,
            TrType = actransaction.TrType,
            TrStatus = actransaction.TrStatus,
            CurrencyType = actransaction.CurrencyType
        };
    }
}
