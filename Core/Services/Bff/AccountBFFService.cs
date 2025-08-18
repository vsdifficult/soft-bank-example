using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using SoftBank.Core.Services.Interfaces;
namespace SoftBank.Core.Services.BFF;

public class AccountBFFService : IAccountBFFService
{
    private readonly ITransactionAccountsRepository _actranzactionRepository;

    public AccountBFFService(ITransactionAccountsRepository actranzactionRepository)
    {
        _actranzactionRepository = actranzactionRepository;
    }
}