namespace SoftBank.Core.Repositories;

public interface IUserRepository : IBaseRepository
{
    void UpdateUserBalance(Guid userId, decimal balance);
}

