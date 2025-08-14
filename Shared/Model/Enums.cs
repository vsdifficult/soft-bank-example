
namespace SoftBank.Shared.Model; 

public enum TransactionStatus
{
    Завершена,
    Ошибка,
    Выполняется,
    Отменена
}
public enum TransactionType
{
    Депозит,
    СнятиеСредств,
    Перевод,
    ДебетоваяОперация
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
