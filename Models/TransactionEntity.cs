public enum TransactionType
{
    Депозит = 1,
    СнятиеСредств = 2,
    Перевод = 3,
    ДебетоваяОперация = 4
}

public enum TransactionStatus
{
    Завершена = 1,
    Ошибка = 2,
    Выполняется = 3,
    Отменена = 4
}

public class TransactionEntity : BaseEntity
{
    public DateTime CommitmentTransaction { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid FromUser { get; set; }
    public Guid ToUser { get; set; }

    public UserEntity? User { get; set; }

    public List<UserEntity> Users { get; set; } = [];

    public Guid FromCard { get; set; }
    public Guid ToCard { get; set; }

    public CardEntity? Card { get; set; }

    public List<CardEntity> Cards { get; set; } = [];

    public TransactionType TrType { get; set; }
    public TransactionStatus TrStatus { get; set; }
}