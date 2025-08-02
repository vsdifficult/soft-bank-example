using Microsoft.EntityFrameworkCore;
using SoftBank.Core.Repositories;
using SoftBank.Infrastructure.EntityFramework;
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