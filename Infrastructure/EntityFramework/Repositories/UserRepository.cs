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
using System.ComponentModel;

namespace SoftBank.Infrastructure.EntityFramework.Repositories
{
    // ����������� ��� ������������
    public class UserRepository : IUserRepository
    {
        private readonly SoftBankDbContext _context;


        public UserRepository(SoftBankDbContext context)
        {
            _context = context;
        }

        // �������� ���� �������������
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(MapToDto);
        }
        public async Task<bool> UpdateUserBalance(Guid userId, decimal balance)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;
            var user_dto = MapToDto(user);
            user_dto.Balance = balance;
            var newB = MapToEntity(user_dto);
            _context.Users.Update(newB);
            await _context.SaveChangesAsync();
            return true;
        }
        // �������� ������������ �� ID
        public async Task<UserDto?> GetByIdAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user == null ? null : MapToDto(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            return await _context.Users.Where(u => u.Email == email).Select(u => new UserDto { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Login = u.Login, Password = u.Password, DateOfBirth = u.DateOfBirth, UserRole = u.UserRole, Code = u.Code }).FirstOrDefaultAsync();
        }

        // ������� ������ ������������
        public async Task<Guid> CreateAsync(UserDto user)
        {
            var userEntity = MapToEntity(user);
            userEntity.Id = Guid.NewGuid(); // ������������� ����� Id

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        // �������� ������������
        public async Task<bool> UpdateAsync(UserDto user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                return false;

            // �������� ����
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Login = user.Login;
            existingUser.Password = user.Password;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<Guid> GetByCardId(Guid cardId)
        {
            var card = _context.Cards.AsNoTracking().FirstOrDefault(c => c.Id == cardId);
            var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == card.UserId);

            return user.Id;
        }

        private async Task<Guid> GetByAccountId(Guid accountId)
        {
            var account = _context.Accounts.AsNoTracking().FirstOrDefault(a => a.Id == accountId);
            var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == account.UserId);

            return user.Id;
        }

        // ������� ������������
        public async Task<bool> DeleteAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // ��������������� �����: ������������� UserEntity � UserDto
        private UserDto MapToDto(UserEntity user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password // � �������� ���������� ������ ������ ������������ � ���� �� ����������
            };
        }

        // ��������������� �����: ������������� UserDto � UserEntity
        private UserEntity MapToEntity(UserDto user)
        {
            return new UserEntity
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password
            };
        }

        // Сделать функцию GetByAccountId
        
        public async Task<List<ClientHistoryPayments>> GetClientHistoryPaymentsAsync(Guid userId)
        {
            // Dtos vars
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            var card = await _context.Cards.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);
            var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.UserId == userId);
            // Dtos Id
            var cardId = card.Id;
            var accountId = account.Id;
            // Exist Check
            if (user == null)
                throw new Exception("User not found.");


            // Creating ClientHistoryPayments from two types of transactions
            var cardTransactions = _context.transactionCards
                .Where(t => t.CardNumberSender == cardId || t.CardNumberRecipient == cardId)
                .Select(t => new ClientHistoryPayments
                {
                    Type = "Card Transaction",
                    userIdSender = Convert.ToString(GetByCardId(t.CardNumberSender)),
                    userIdRecipient = Convert.ToString(GetByCardId(t.CardNumberRecipient)),
                    Amount = t.Amount,
                    TransactionId = t.Id,
                    Date = t.CommitmentTransaction
                });

            var accountTransactions = _context.transactionAccounts
                .Where(t => t.AccountNumberSender == accountId || t.AccountNumberRecipient == accountId)
                .Select(t => new ClientHistoryPayments
                {
                    Type = "Account Transaction",
                    userIdSender = Convert.ToString(GetByCardId(t.AccountNumberSender)),
                    userIdRecipient = Convert.ToString(GetByCardId(t.AccountNumberRecipient)),
                    Amount = t.Amount,
                    TransactionId = t.Id,
                    Date = t.CommitmentTransaction
                });

            // Сombine both types of Histories and return the result.
            var combinedQuery = cardTransactions.Union(accountTransactions);


            return (List<ClientHistoryPayments>)combinedQuery;
        }
    }
}

