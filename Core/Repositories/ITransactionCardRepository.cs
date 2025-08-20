using SoftBank.Core.Repositories;
using SoftBank.Shared.Dto;
namespace SoftBank.Core.Repositories;

public interface ITransactionCardRepository : IRepository<TransactionDto, Guid>
{

}