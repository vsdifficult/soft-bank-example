using Microsoft.AspNetCore.Mvc;
using SoftBank.Core.Services.Interfaces;
using SoftBank.Shared.Model;
using SoftBank.Core.Services.BFF;
using SoftBank.Shared.Dto;

namespace SoftBank.Api.Contollers;

[ApiController]
[Route("api/client")]
public class ClientEnpoints : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly IClientBFFService _clientBFFService;
    public ClientEnpoints(IDataService dataService, IClientBFFService clientBFFService)
    {
        _dataService = dataService;
        _clientBFFService = clientBFFService;
    }

    [HttpPost("get/{Id}/")]
    public async Task<IActionResult> EndpointGetUsersForUserAsync(Guid userId)
    {
        try
        {
            var user = await _clientBFFService.GetAccountsForUser(userId);
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
            return Ok(cards);
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
            return Ok(history);
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
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}