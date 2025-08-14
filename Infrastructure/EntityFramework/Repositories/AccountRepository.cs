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

public class UserRepository : IUserRepository
{   
    private readonly SoftBankDbContext _context;

    public AccountRepository(SoftBankDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountDto>> GetAllAsync()
    {
        var account = await _context.Accounts.ToListAsync();
        return accounts.Select(MapToDto);
    }

    public async Task<AccountDto?> GetByIdAsync(Guid accountId)
    {
        var account = await _context.Accounts.FindAsync(accountId);
        return account == null ? null : MapToDto(account);
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