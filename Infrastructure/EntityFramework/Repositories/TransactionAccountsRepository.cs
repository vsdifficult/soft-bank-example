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

public class TransactionAccountsRepository : ITransactionAccountsRepository
{   
    private readonly SoftBankDbContext _context;

    public TransactionAccountsRepository(SoftBankDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionAccountDto>> GetAllAsync()
    {                                      
        var actransaction = await _context.transactionAccounts.ToListAsync();
        return actransaction.Select(MapToDto);
    }


    public async Task<TransactionAccountDto?> GetByIdAsync(Guid transactionAccountId)
    {
        var actransaction = await _context.transactionAccounts.FindAsync(transactionAccountId);
        return actransaction == null ? null : MapToDto(actransaction);
    }

    public async Task<Guid> CreateAsync(TransactionAccountDto transactionAccountDto)
    {
        var actransactionEntity = MapToEntity(transactionAccountDto);
        actransactionEntity.Id = Guid.NewGuid(); 

        await _context.transactionAccounts.AddAsync(actransactionEntity);
        await _context.SaveChangesAsync();

        return actransactionEntity.Id;
    }
    public async Task<bool> UpdateAsync(TransactionAccountDto transactionAccountDto)
    { 
        var transaction = await _context.transactionAccounts.FindAsync(transactionAccountDto.Id);
        if (transaction == null) return false;

        _context.transactionAccounts.Update(MapToEntity(transactionAccountDto));
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(Guid transactionAccountId)
    {
        var actransaction = await _context.transactionAccounts.FindAsync(transactionAccountId);
        if (actransaction == null)
            return false;

        _context.transactionAccounts.Remove(actransaction);
        await _context.SaveChangesAsync();
        return true;
    }

    private TransactionAccountDto MapToDto(TransactionAccount transactionAccount)
    {
        return new TransactionAccountDto
        {
            Id = transactionAccount.Id,
            CommitmentTransaction = transactionAccount.CommitmentTransaction,
            Amount = transactionAccount.Amount,
            Description = transactionAccount.Description, 
            AccountNumberRecipient = transactionAccount.AccountNumberRecipient ,
            AccountNumberSender = transactionAccount.AccountNumberSender,
            TrType = transactionAccount.TrType,
            TrStatus = transactionAccount.TrStatus,
            CurrencyType = transactionAccount.CurrencyType
        };
    }

    private TransactionAccount MapToEntity(TransactionAccountDto transactionAccountDto)
    {
        return new TransactionAccount
        {
            Id = transactionAccountDto.Id,
            CommitmentTransaction = transactionAccountDto.CommitmentTransaction,
            Amount = transactionAccountDto.Amount,
            Description = transactionAccountDto.Description,
            AccountNumberRecipient = transactionAccountDto.AccountNumberRecipient ?? Guid.Empty,
            AccountNumberSender = transactionAccountDto.AccountNumberSender ?? Guid.Empty,
            TrType = transactionAccountDto.TrType,
            TrStatus = transactionAccountDto.TrStatus,
            CurrencyType = transactionAccountDto.CurrencyType
        };
    }
}
