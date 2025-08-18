using Microsoft.AspNetCore.Mvc;
using SoftBank.Core.Repositories;
using SoftBank.Shared.Model;

namespace SoftBank.Api.Contollers;

[ApiController]
[Route("api/account")]
public class AccountEnpoints : ControllerBase
{
    private readonly IAccountRepository _accountRepo; 
    public UserEndpoint(IAccountRepository accountRepo)
    {
        _accountRepo = accountRepo;
    }

    [HttpPost("create")]
    public async Task<IActionResult> EndpointCreateAccountAsync(RegisterAccountDto dto) // создать дто для регистрации аккаунта (создании) которое будет состоять из полей: Название счета, id владельца
    {
        try
        {
            var user = await _accountRepo.CreateAsync(dto);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    
}
