using SoftBank.Core.Repositories;
<<<<<<< HEAD
using SoftBank.Shared.Dto;
=======
using SoftBank.Shared.Dto; 
>>>>>>> ffd8a5c1a5f383a6744c72f7c46bc3b739170361
namespace SoftBank.Core.Repositories;

public interface IUserRepository : IRepository<UserDto, Guid>
{
    void UpdateUserBalance(Guid userId, decimal balance);
    Task<UserDto> GetByEmailAsync (string email);
}
