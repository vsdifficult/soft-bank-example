
namespace SoftBank.Shared.Model; 

public enum TransactionStatus
{
<<<<<<< HEAD
    Завершена = 1,
    Ошибка = 2,
    Выполняется = 3,
    Отменена = 4
}
public enum TransactionType
{
    Депозит = 1,
    СнятиеСредств = 2,
    Перевод = 3,
    ДебетоваяОперация = 4
=======
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

>>>>>>> ffd8a5c1a5f383a6744c72f7c46bc3b739170361
}