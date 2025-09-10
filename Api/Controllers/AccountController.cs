using Microsoft.AspNetCore.Mvc;
using SoftBank.Core.Services.Interfaces;
using SoftBank.Shared.Dto;

namespace SoftBank.Api.Contollers;

[ApiController]
[Route("api/account")]
public class AccountEnpoints : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly IAccountBFFService _accountBFFService;
    public AccountEnpoints(IDataService dataService, IAccountBFFService accountBFFService)
    {
        _dataService = dataService;
        _accountBFFService = accountBFFService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> EndpointCreateAccountAsync(AccountDto dto)
    {
        try
        {
            var user = await _dataService.accountRepository.CreateAsync(dto);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("get/{Id}/statistics")]
    public async Task<IActionResult> EndpointStatiscicsAccountAsync(Guid accountId)
    {
        try
        {
            var user = await _accountBFFService.GetStatistics(accountId);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
