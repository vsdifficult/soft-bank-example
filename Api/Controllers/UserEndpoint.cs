using Microsoft.AspNetCore.Mvc;
using SoftBank.Core.Services.Interfaces;
using SoftBank.Shared.Model;

namespace SoftBank.Api.Contollers;

[ApiController]
[Route("api/auth")]
public class UserEndpoint : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public UserEndpoint(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> EndpointSignUpAsync(RegisterDto dto)
    {
        try
        {
            var user = await _authenticationService.SignUpAsync(dto);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    
    [HttpPost("verification")]
    public async Task<IActionResult> EndpointVerifyAsync(VerificationDto dto)
    {
        try
        {
            var user = await _authenticationService.VerificationAsync(dto);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
}