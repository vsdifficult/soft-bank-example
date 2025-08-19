using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
namespace SoftBank.Core.Services.Interfaces;

public interface IClientService
{
    Task<TransactionDto> TransactionTransferAsync(TransferDto transfer);
}
