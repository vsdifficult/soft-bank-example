using Microsoft.AspNetCore.Mvc;
using SoftBank.Core.Services.Interfaces;
using SoftBank.Shared.Model;
using SoftBank.Core.Services.Bff;

namespace SoftBank.Api.Contollers;

[ApiController]
[Route("api/client")]
public class ClientEnpoints : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly IClientBFFService _clientBFFService;
    public AccountEnpoints(IDataService dataService, IClientBFFService clientBFFService)
    {
        _dataService = dataService;
        _clientBFFService = clientBFFService;
    }

    [HttpPost("get/{Id}/accounts")]
    public async Task<IActionResult> EndpointGetAccountsForUserAsync(Guid userId)
    {
        try
        {
            var accounts = await _clientBFFService.GetAccountsForUser(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("get/{Id}/cards")]
    public async Task<IActionResult> EndpointGetCardsForUserAsync(Guid userId)
    {
        try
        {
            var cards = await _clientBFFService.GetCardsForUser(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("history")]
    public async Task<IActionResult> EndpointHistoryOfTransactionsAsync(Guid userId)
    {
        try
        {
            var history = await _clientBFFService.HistoryOfTransactions(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> EndpointTransactionTransferAsync(TransferDto transfer)
    {
        try
        {
            var transaction = await _clientBFFService.TransactionTransferAsync(transfer);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}