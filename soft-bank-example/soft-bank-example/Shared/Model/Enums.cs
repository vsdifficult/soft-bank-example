
namespace SoftBank.Shared.Model; 

public enum TransactionStatus
{
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
}