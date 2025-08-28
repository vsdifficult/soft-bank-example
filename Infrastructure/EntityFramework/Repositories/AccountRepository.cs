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

public class AccountRepository : IAccountRepository
{   
    private readonly SoftBankDbContext _context;

    public AccountRepository(SoftBankDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountDto>> GetAllAsync()
    {
        var account = await _context.Accounts.ToListAsync();
        return account.Select(MapToDto);
    }

    public async Task<AccountDto?> GetByIdAsync(Guid accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        return account == null ? null : MapToDto(account);
    }

    public async Task<Guid> CreateAsync(AccountDto account)
    {
        var accountEntity = MapToEntity(account);
        accountEntity.Id = Guid.NewGuid(); 

        await _context.Accounts.AddAsync(accountEntity);
        await _context.SaveChangesAsync();

        return accountEntity.Id;
    }

    public async Task<bool> DeleteAsync(Guid accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        if (account == null)
            return false;

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateAsync(AccountDto accountDto)
    {
        var account = await _context.Accounts.FindAsync(accountDto.Id);
        if (account == null) return false;

        _context.Accounts.Update(MapToEntity(accountDto));
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<AccountStatisticsDto> GetAccountStatistics(Guid accountId)
    { 

        // Creating variables with LINQ references
        var createdAt = DateTime.Now;
        var transationsQuantity = _context.transactionAccounts.Count();
        var spendAmount = _context.transactionAccounts.Where(t => t.Amount < 0).Sum(t => (decimal?)t.Amount * -1) ?? 0m;
        var earnAmount = _context.transactionAccounts.Where(t => t.Amount > 0).Sum(t => (decimal?)t.Amount) ?? 0m;
        var transactionsHistory = _context.transactionAccounts
                    .OrderByDescending(t => t.CommitmentTransaction)
                    .Select(t => new TransactionAccountDto
                    {
                        Id = t.Id,
                        Amount = t.Amount,
                        CommitmentTransaction = t.CommitmentTransaction,
                        Description = t.Description,
                        CurrencyType = t.CurrencyType,
                        TrType = t.TrType,
                        TrStatus = t.TrStatus,
                        AccountNumberSender = t.AccountNumberSender,
                        AccountNumberRecipient = t.AccountNumberRecipient
                    }).ToList();

        // Return of the completed AccountStatisticsDto
        return new AccountStatisticsDto
        {
            AccountId = accountId,
            CreatedAt = createdAt,
            TransationsQuantity = transationsQuantity,
            SpendAmount = spendAmount,
            EarnAmount = earnAmount,
            TransactionsHistory = transactionsHistory
        };
    }

    public async Task<List<AccountDto>> GetAccountsForUserAsync(Guid userId)
    {
        return await _context.Accounts.AsNoTracking()
            .Where(u => u.UserId == userId)
            .Select(u => new AccountDto
            {
                Id = u.Id,
                UserId = u.UserId,
                Cards = u.Cards,
                AccountType = u.AccountType,
                CurrencyType = u.CurrencyType,
                Amount = u.Amount,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();
    } 

    private AccountDto MapToDto(AccountEntity account)
    {
        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            Cards = account.Cards,
            AccountType = account.AccountType,
            CurrencyType = account.CurrencyType,
            Amount = account.Amount,
            CreatedAt = account.CreatedAt
        };
    }

    private AccountEntity MapToEntity(AccountDto account)
    {
        return new AccountEntity
        {
            Id = account.Id,
            UserId = account.UserId,
            Cards = account.Cards,
            AccountType = account.AccountType,
            CurrencyType = account.CurrencyType,
            Amount = account.Amount,
            CreatedAt = account.CreatedAt
        };
    }
}
