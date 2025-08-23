using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using SoftBank.Core.Services.Interfaces;
using SoftBank.Core.Repositories;

namespace SoftBank.Core.Services.BFF;

public class AccountBFFService : IAccountBFFService
{
    private readonly ITransactionAccountsRepository _actransactionRepository; 
    private readonly IUserRepository _userRepository; 
    private readonly IAccountRepository _accountRepository;

    public AccountBFFService(ITransactionAccountsRepository actransactionRepository, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _actransactionRepository = actransactionRepository;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public async Task<TransactionAccountDto> ProcessPayment(PaymentDto payment)
    {
        var accountSender = await _actransactionRepository.GetByIdAsync(payment.AccountNumberSender);

        if (accountSender == null)
        {
            throw new Exception("AccountSender not found.");
        }

        var accountRecipient = await _actransactionRepository.GetByIdAsync(payment.AccountNumberRecipient);

        if (accountRecipient == null)
        {
            throw new Exception("accountRecipient not found.");
        }

        if (accountSender.CurrencyType != accountRecipient.CurrencyType)
        {
            throw new Exception("CurrencyType is not available.");
        }

        var actransaction = new TransactionAccountDto
        {
            Id = Guid.NewGuid(),
            CommitmentTransaction = DateTime.Now,
            Amount = payment.Amount,
            TrType = TransactionType.Transfer,
            TrStatus = TransactionStatus.Pending,
            CurrencyType = payment.CurrencyType,
            AccountNumberSender = payment.AccountNumberSender,
            AccountNumberRecipient = payment.AccountNumberRecipient
        };

        await _userRepository.UpdateUserBalance(accountSender.Id, accountSender.Amount - payment.Amount);
        await _userRepository.UpdateUserBalance(accountRecipient.Id, accountRecipient.Amount + payment.Amount);

        await _actransactionRepository.CreateAsync(actransaction);

        return actransaction;
    }


    public async Task<AccountStatisticsDto> GetStatistics(Guid accountId)
    {
        return await _accountRepository.GetAccountStatistics(accountId);
    }
}