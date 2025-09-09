using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using SoftBank.Core.Services.Interfaces;
using SoftBank.Core.Email;
using Microsoft.VisualBasic;

namespace SoftBank.Core.Services.BFF;

public class ClientBFFService : IClientBFFService
{
    private readonly IDataService _dataService;
    private readonly ICardBFFService _cardBFFService;
    private readonly IAccountBFFService _accountBFFService;
    private readonly EmailSenderService _emailSenderService;

    public ClientBFFService(IDataService dataService, ICardBFFService cardBFFService,
    IAccountBFFService accountBFFService, EmailSenderService emailSenderService)
    {
        _dataService = dataService;
        _cardBFFService = cardBFFService;
        _accountBFFService = accountBFFService;
        _emailSenderService = emailSenderService;
    }

    public async Task<TransactionDto> TransactionTransferAsync(TransferDto transfer)
    {
        var sender = await _dataService.userRepository.GetByIdAsync(transfer.SenderId);
        var recipient = await _dataService.userRepository.GetByIdAsync(transfer.RecipientId);

        if (transfer.TransferType == TransferType.Card)
        {
            await _cardBFFService.ProcessPayment(
                new CardPaymentDto
                {
                    Amount = transfer.Amount,
                    CurrencyType = transfer.CurrencyType,
                    CardNumberSender = transfer.CardSenderId,
                    CardNumberRecipient = transfer.CardRecipientId
                }
            );

            return new TransactionDto
            {
                Id = Guid.NewGuid(),
                SenderId = transfer.SenderId,
                RecipientId = transfer.RecipientId,
                Date = DateTime.UtcNow,
                Amount = transfer.Amount,
                Status = TransactionStatus.Finished,
                CurrencyType = transfer.CurrencyType,
                Ttype = TransferType.Card
            };
        }
        else if (transfer.TransferType == TransferType.Account)
        {
            await _accountBFFService.ProcessPayment(
                new PaymentDto
                {
                    Amount = transfer.Amount,
                    CurrencyType = transfer.CurrencyType,
                    AccountNumberSender = transfer.AccountSenderId,
                    AccountNumberRecipient = transfer.AccountRecipientId
                }
            );

            return new TransactionDto
            {
                Id = Guid.NewGuid(),
                SenderId = transfer.SenderId,
                RecipientId = transfer.RecipientId,
                Date = DateTime.UtcNow,
                Amount = transfer.Amount,
                Status = TransactionStatus.Finished,
                CurrencyType = transfer.CurrencyType,
                Ttype = TransferType.Account
            };
        }

        await _emailSenderService.SendEmailAsync(sender.Email, "TransactionError", $"{transfer.CardSenderId}\n{transfer.AccountSenderId}");

        throw new Exception("TransactionError");
    }

    public async Task<List<ClientHistoryPayments>> HistoryOfTransactions(Guid userId)
    {
        return await _dataService.userRepository.GetClientHistoryPaymentsAsync(userId);
    }

    public async Task<IEnumerable<AccountDto>> GetAccountsForUser(Guid userId)
    {
        return await _dataService.accountRepository.GetAccountsForUserAsync(userId);
    }

    public async Task<IEnumerable<CardDto>> GetCardsForUser(Guid userId)
    {
        return await _dataService.cardRepository.GetCardsForUserAsync(userId);
    }
    
}