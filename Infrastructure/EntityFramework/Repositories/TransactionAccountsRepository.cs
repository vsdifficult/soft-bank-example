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
    {                                       // Why are AccounT. In DbContext it named Accounts 
        var actransaction = await _context.transactionAccounts.ToListAsync();
        return actransaction.Select(MapToDto);
    }

    public async Task<TransactionAccountDto?> GetByIdAsync(Guid actransactionId)
    {
        var actransaction = await _context.transactionAccounts.FindAsync(actransactionId);
        return actransaction == null ? null : MapToDto(actransaction);
    }

    public async Task<Guid> CreateAsync(TransactionAccountDto actransaction)
    {
        var actransactionEntity = MapToEntity(actransaction);
        actransactionEntity.Id = Guid.NewGuid(); 

        await _context.transactionAccounts.AddAsync(actransactionEntity);
        await _context.SaveChangesAsync();

        return actransactionEntity.Id;
    }
    public async Task<bool> UpdateAsync(TransactionAccountDto accountDto)
    { 
        var transaction = await _context.transactionAccounts.FindAsync(accountDto.Id);
        if (transaction == null) return false;

        _context.transactionAccounts.Update(MapToEntity(accountDto));
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(Guid actransactionId)
    {
        var actransaction = await _context.transactionAccounts.FindAsync(actransactionId);
        if (actransaction == null)
            return false;

        _context.transactionAccounts.Remove(actransaction);
        await _context.SaveChangesAsync();
        return true;
    }

    private TransactionAccountDto MapToDto(TransactionAccount actransaction)
    {
        return new TransactionAccountDto
        {
            Id = actransaction.Id,
            CommitmentTransaction = actransaction.CommitmentTransaction,
            Amount = actransaction.Amount,
            Description = actransaction.Description, 
            AccountNumberRecipient = actransaction.AccountNumberRecipient,
            AccountNumberSender = actransaction.AccountNumberSender,
            TrType = actransaction.TrType,
            TrStatus = actransaction.TrStatus,
            CurrencyType = actransaction.CurrencyType
        };
    }

    // Why are endwith Entity, if class had been name "TransactionAccount"
    private TransactionAccount MapToEntity(TransactionAccountDto actransaction)
    {
        return new TransactionAccount
        {
            Id = actransaction.Id,
            CommitmentTransaction = actransaction.CommitmentTransaction,
            Amount = actransaction.Amount,
            Description = actransaction.Description,
            AccountNumberRecipient = actransaction.AccountNumberRecipient,
            AccountNumberSender = actransaction.AccountNumberSender,
            TrType = actransaction.TrType,
            TrStatus = actransaction.TrStatus,
            CurrencyType = actransaction.CurrencyType
        };
    }
}
