using SoftBank.Core.Services.Interfaces;
using SoftBank.Core.Repositories;
using SoftBank.Shared.Dto;


namespace SoftBank.Infrastructure.Auth;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Функция регистрации пользователя
    public async Task<AuthResult> SignUpAsync(RegisterDto dto)
    {
        // Проверка есть ли user в БД
        var user = await _userRepository.FindByIdAsync(dto.Id);
        if (user != null)
        {
            return new AuthResult {  Success = false, ErrorMessage = "Пользователь с таким Id уже есть."};
        }

        // Код для верификации
        var verifyCode = new Random().Next(1000, 9999);

        // Создание Дто для репозитория
        var user = new UserDto { 
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName, 
            LastName = dto.LastName, 
            Email = dto.Email,
            Login = dto.Login,
            Password = dto.Password,
            DateOfBirth = dto.DateOfBirth,
            UserRole = dto.Role,
            Code = verifyCode
        }; 

        // Отправка кода на почту


        // Создание Entity
        await _userRepository.CreateAsync(user);

        // Создание токена
        // var token = await GenerateTokenAsync(user.Id, user.Role);

        // Возврат результата
        return new AuthResult
        {
            Success = true,
            Message = "Регистрация прошла успешно."
        };
    }

    // Функция удаления пользователя
    public async Task<AuthResult> DeleteAsync(Guid userId)
    {
        // Проверка есть ли user в БД
        var user = await _userRepository.FindByIdAsync(userId);
        if (user == null)
        {
            return new AuthResult { Success = false, ErrorMessage = "Пользователь с таким Id не найден." };
        }

        // Удаление user
        await _userRepository.DeleteAsync(user);

        // Возврат результата
        return new AuthResult
        {
            Success = true,
            Message = "Удаление прошло успешно."
        };
    }


    // -------- Доп. функции -------

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

}

