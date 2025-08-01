namespace SoftBank.Core.Repositories;

public interface IUserRepository : IBaseRepository
{
    void UpdateUserBalance(int userId, decimal balance);
}

