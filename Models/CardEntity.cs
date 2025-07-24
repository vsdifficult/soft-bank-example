public class CardEntity : BaseEntity
{
    public string CardNumber { get; set; } = string.Empty;

    public string CardHolderName { get; set; } = string.Empty;

    public DateTime ExpirationDate { get; set; }

    public string CVV { get; set; } = string.Empty;

    public UserEntity? User { get; set; }
    public Guid UserId { get; set; }
}