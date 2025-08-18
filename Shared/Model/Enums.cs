
namespace SoftBank.Shared.Model; 

public enum TransactionStatus
{
    Finished,
    Error,
    Pending,
    Denied
}
public enum TransactionType
{
    Deposite,
    Withdrawal,
    Transfer,
    DebitOper
}

public enum UserRole
{
    Admin,
    User

}

public enum Account
{
    Debet,
    Cumulative,
    Credit
}

public enum Currency
{
    Dollar,
    Euro
}

public enum TransferType
{
    Card,
    Account
}

