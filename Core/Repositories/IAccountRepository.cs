using SoftBank.Core.Repositories;
using SoftBank.Shared.Dto; 
namespace SoftBank.Core.Repositories;

public interface IAccountRepository : IRepository<AccountDto, Guid>
{

}