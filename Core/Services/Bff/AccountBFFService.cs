using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using SoftBank.Core.Services.Interfaces;

namespace SoftBank.Core.Services.BFF;

public class AccountBFFService : IAccountBFFService
{
    private readonly ITransactionAccountsRepository _actransactionRepository;

    public AccountBFFService(ITransactionAccountsRepository actransactionRepository)
    {
        _actransactionRepository = actransactionRepository;
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
            TrType = TransactionType.Trasfer,
            TrStatus = TransactionStatus.Pending,
            CurrencyType = payment.CurrencyType,
            AccountNumberSender = payment.AccountNumberSender,
            AccountNumberRecipient = payment.AccountNumberRecipient
        };

        accountSender.Amount -= payment.Amount;
        accountRecipient.Amount += payment.Amount;
        
        await _actransactionRepository.CreateAsync(actransaction);

        return actransaction;
    }


    public async Task<AccountStatisticsDto> GetStatistics(Guid accountId)
    {
        // Var of account
        var account = await _actransactionRepository.GetByIdAsync(accountId);

        // Creating variables with LINQ references
        var createdAt = DateTime.Now;
        var transationsQuantity = account.Transactions.Count();
        var spendAmount = account.Transactions.Where(t => t.Amount < 0).Sum(t => (decimal?)t.Amount * -1) ?? 0m;
        var earnAmount = account.Transactions.Where(t => t.Amount > 0).Sum(t => (decimal?)t.Amount) ?? 0m;
        var transactionsHistory = account.Transactions
                    .OrderByDescending(t => t.TransactionDate)
                    .Select(t => new TransactionAccountDto
                    {
                        TransactionId = t.Id,
                        Amount = t.Amount,
                        TransactionDate = t.TransactionDate,
                        Description = t.Description
                    }).ToList();

        // Return of the completed AccountStatisticsDto
        return new AccountStatisticsDto
        {
            AccountId = accountId,
            CreatedAt = createdAt,
            TransactionsQuantity = transationsQuantity,
            SpendAmount = spendAmount,
            EarnAmount = earnAmount,
            TransactionsHistory = transactionsHistory
        };
    }
}