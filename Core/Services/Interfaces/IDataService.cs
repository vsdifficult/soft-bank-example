using SoftBank.Core.Repositories;
using SoftBank.Shared.Dto; 

namespace SoftBank.Core.Services.Interfaces;

public interface IDataService
{
    IUserRepository userRepository { get; } 
    IAccountRepository accountRepository { get; }
    ICardRepository cardRepository { get; }
    ITransactionCardRepository transactionCardRepository { get; }
    ITransactionAccountsRepository transactionAccountsRepository { get; }
}


