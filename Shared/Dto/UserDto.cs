using SoftBank.Shared.Model; 
namespace SoftBank.Shared.Dto;


public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public decimal Balance { get; set; }

    public DateTime DateOfBirth { get; set; }

    public UserRole UserRole { get; set; }

    public int Code { get; set; }
    
    // public List<CardEntity> Cards { get; set; } = [];
}

public record ClientStatisticsDto
{
    public Guid UserId { get; init; }

    public AccountStatisticsDto AccountStatistics { get; init; }

    public CardStatisticsDto CardStatistics { get; init; }
}


// User DTO register 


// User DTO login


// User DTO Update

public record RegisterDto
{
    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Login { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public DateTime DateOfBirth { get; init; }

    public UserRole UserRole { get; init; }
}

public record LoginDto
{
    public string Password { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;
}

public record VerificationDto
{
    public string Email { get; init; } = string.Empty;

    public int Code { get; init; }
}