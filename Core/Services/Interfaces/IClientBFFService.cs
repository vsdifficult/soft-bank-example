using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
namespace SoftBank.Core.Services.Interfaces;

public interface IClientBFFService
{
    Task<TransactionDto> TransactionTransferAsync(TransferDto transfer);

    Task HistoryOfTransactions(Guid userId);

    
}
