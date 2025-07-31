namespace SoftBank.Infrastructure.Entities;
public class UserEntity : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public List<CardEntity> Cards { get; set; } = [];

    // public TransactionEntity? Transaction { get; set; }

    public List<TransactionEntity> Transactions { get; set; } = [];
}
