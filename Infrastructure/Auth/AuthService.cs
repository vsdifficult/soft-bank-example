using SoftBank.Core.Services.Interfaces;
using SoftBank.Core.Repositories;
using SoftBank.Shared.Dto;
using SoftBank.Shared.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace SoftBank.Infrastructure.Auth;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // ������� ����������� ������������
    public async Task<AuthResult> SignUpAsync(RegisterDto dto)
    {
        // �������� ���� �� user � ��
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user != null)
        {
            return new AuthResult {  Success = false, ErrorMessage = "������������ � ����� Id ��� ����."};
        }

        // ��� ��� �����������
        var verifyCode = new Random().Next(1000, 9999);

        // �������� ��� ��� �����������
        var user1 = new UserDto { 
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName, 
            LastName = dto.LastName, 
            Email = dto.Email,
            Login = dto.Login,
            Password = dto.Password,
            DateOfBirth = dto.DateOfBirth,
            UserRole = dto.UserRole,
            Code = verifyCode
        }; 

        // �������� ���� �� �����


        // �������� Entity
        await _userRepository.CreateAsync(user1);

        // �������� ������
        // var token = await GenerateTokenAsync(user.Id, user.Role);

        // ������� ����������
        return new AuthResult
        {
            Success = true,
            Message = "����������� ������ �������."
        };
    }

    // ������� �������� ������������
    public async Task<AuthResult> DeleteAsync(Guid userId)
    {
        // �������� ���� �� user � ��
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new AuthResult { Success = false, ErrorMessage = "������������ � ����� Id �� ������." };
        }

        // �������� user
        await _userRepository.DeleteAsync(user.Id);

        // ������� ����������
        return new AuthResult
        {
            Success = true,
            Message = "�������� ������ �������."
        };
    }


    // -------- ���. ������� -------

    private async Task<string> GenerateTokenAsync(Guid userId, UserRole role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("dslkfnslkfnlsjdf");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Role, role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(62),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public async Task<AuthResult> SignInAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
        {
            return new AuthResult { Success = false, ErrorMessage = "User not found." };
        }
        else if(user.Password == dto.Password)
        {
            var token = await GenerateTokenAsync(user.Id, user.UserRole);
            return new AuthResult{Success = true};
        }

        return new AuthResult { Success = false, ErrorMessage = "Login error." };
    }

    public async Task<AuthResult> VerificationAsync(VerificationDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
        {
            return new AuthResult {Success = false, ErrorMessage = "User not found."};
        }
        
        if (dto.Code == user.Code)
        {
            var token = await GenerateTokenAsync(user.Id, user.UserRole);
            return new AuthResult { Success = true, UserId = user.Id, Token = token, Role = user.UserRole };
        }
        else
        {
            return new AuthResult { Success = false, ErrorMessage = "Incorrect code." };
        }
    }
}
