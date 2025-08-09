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
            return await _context.Users.Where(u => u.Email==email).Select(u => new UserDto{Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Login = u.Login, Password = u.Password, DateOfBirth = u.DateOfBirth, UserRole = u.UserRole, Code = u.Code}).FirstOrDefaultAsync(); 
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
    }
}
