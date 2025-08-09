
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