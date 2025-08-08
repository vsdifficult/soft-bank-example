using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
namespace SoftBank.Core.Services.Interfaces;

public record AuthResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public string? Message { get; init; }
    public Guid? UserId { get; init; }
    public string? Token { get; init; }
    public int Code { get; init; }
    public UserRole? Role { get; init; }
}

/// <summary>
/// Service for authentication operations
/// </summary>
public interface IAuthenticationService
{
    Task<AuthResult> SignUpAsync(RegisterDto dto);
    Task<AuthResult> SignInAsync(LoginDto dto);
    Task<AuthResult> VerificationAsync(VerificationDto dto);
    Task<AuthResult> DeleteAsync(Guid userid);
    
}
