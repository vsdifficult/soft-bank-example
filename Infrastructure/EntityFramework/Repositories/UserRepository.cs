using Microsoft.EntityFrameworkCore;
using SoftBank.Core.Repositories;
using SoftBank.Infrastructure.EntityFramework;
<<<<<<< HEAD
using SoftBank.Infrastructure.Entities;
using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftBank.Infrastructure.EntityFramework.Repositories
{
    // Репозиторий для пользователя
    public class UserRepository : IUserRepository
    {
        private readonly SoftBankDbContext _context;

        public UserRepository(SoftBankDbContext context)
        {
            _context = context;
        }

        // Получить всех пользователей
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(MapToDto);
        }

        // Получить пользователя по ID
        public async Task<UserDto?> GetByIdAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user == null ? null : MapToDto(user);
        }

        // Создать нового пользователя
        public async Task<Guid> CreateAsync(UserDto user)
        {
            var userEntity = MapToEntity(user);
            userEntity.Id = Guid.NewGuid(); // сгенерировать новый Id

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        // Обновить пользователя
        public async Task<bool> UpdateAsync(UserDto user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                return false;

            // Обновить поля
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Login = user.Login;
            existingUser.Password = user.Password;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить пользователя
        public async Task<bool> DeleteAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Вспомогательный метод: преобразовать UserEntity в UserDto
        private UserDto MapToDto(UserEntity user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password // В реальном приложении пароль хранят хэшированным и сюда не возвращают
            };
        }

        // Вспомогательный метод: преобразовать UserDto в UserEntity
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
=======
using SoftBank.Infrastructure.Entities; 
using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
namespace SoftBank.Infrastructure.EntityFramework.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SoftBankDbContext _context;
    public UserRepository(SoftBankDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> GetByIdAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user == null ? null : MapToDto(user);
    }

    public async Task<Guid> CreateAsync(UserDto user)
    {
        var user_ = new UserDto
        {
            Id = Guid.NewGuid(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Login = user.Login,
            Password = user.Password
        };

        await _context.Users.AddAsync(MapToEntity(user_));
        await _context.SaveChangesAsync();
        return user_.Id;
    }
    private UserEntity MapToEntity(UserDto user)
    {
        return new UserEntity
        {
            
        };
    }
    private UserDto MapToDto(UserEntity user)
    {
        return new UserDto
        {

        }; 
    } 
}
>>>>>>> ffd8a5c1a5f383a6744c72f7c46bc3b739170361
