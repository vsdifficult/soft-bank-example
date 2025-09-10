
using SoftBank.Core.Repositories;
using SoftBank.Core.Services.Interfaces;

namespace SoftBank.Infrastructure.Services
{
    public class DataService : IDataService
    {
        public DataService(IUserRepository userRepository, IAccountRepository accountRepository, ICardRepository cardRepository, ITransactionCardRepository transactionCardRepository, ITransactionAccountsRepository transactionAccountsRepository)
        {
            this.userRepository = userRepository;
            this.accountRepository = accountRepository;
            this.cardRepository = cardRepository;
            this.transactionCardRepository = transactionCardRepository;
            this.transactionAccountsRepository = transactionAccountsRepository;
        }

        public IUserRepository userRepository { get; }
        public IAccountRepository accountRepository { get; }
        public ICardRepository cardRepository { get; }
        public ITransactionCardRepository transactionCardRepository { get; }
        public ITransactionAccountsRepository transactionAccountsRepository { get; }
    }
}
