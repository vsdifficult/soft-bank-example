using SoftBank.Core.Repositories;
using SoftBank.Shared.Dto; 
namespace SoftBank.Core.Repositories;

public interface IUserRepository : IRepository<UserDto, Guid>
{
    Task<bool> UpdateUserBalance(Guid userId, decimal balance);
    Task<UserDto> GetByEmailAsync(string email);
    Task<List<ClientHistoryPayments>> GetClientHistoryPaymentsAsync(Guid userId);
}
