using SoftBank.Core.Repositories;
using SoftBank.Shared.Dto; 
namespace SoftBank.Core.Repositories;

public interface IUserRepository : IRepository<UserDto, Guid>
{
    void UpdateUserBalance(Guid userId, decimal balance);
}
