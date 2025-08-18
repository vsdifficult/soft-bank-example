using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
namespace SoftBank.Core.Services.Interfaces;

public interface IAccountBFFService
{
    Task<TransactionAccountDto> ProcessPayment(PaymentDto payment);
    
    Task<AccountStatisticsDto> GetStatistics(Guid accountId);
}