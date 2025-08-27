using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
namespace SoftBank.Core.Services.Interfaces;

public interface IClientBFFService
{
    Task<TransactionDto> TransactionTransferAsync(TransferDto transfer);
    Task<List<ClientHistoryPayments>> HistoryOfTransactions(Guid userId);
    Task<IEnumerable<AccountDto>> GetAccountsForUser(Guid userId);
    Task<IEnumerable<CardDto>> GetCardsForUser(Guid userId);
}
