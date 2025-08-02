
namespace SoftBank.Shared.Dto;


public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    // public List<CardEntity> Cards { get; set; } = [];
} 

// User DTO register 


// User DTO login


// User DTO Update