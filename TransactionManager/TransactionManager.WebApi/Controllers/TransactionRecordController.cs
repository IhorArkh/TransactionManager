using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionManager.Application.Features.TransactionRecord.Commands.AddTransactionRecord;
using TransactionManager.Application.Features.TransactionRecord.Queries.GetTransactionsOccuredInClientsTimeZone;
using TransactionManager.Application.Features.TransactionRecord.Queries.GetTransactionsOccuredInUsersTimeZone;

namespace TransactionManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionRecordController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionRecordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Adds transactions from CSV to database.
    /// </summary>
    /// <response code="200">Added successfully.</response>
    /// <response code="400">If file not provided.</response>
    [HttpPost]
    public async Task<IActionResult> AddTransactions(IFormFile file)
    {
        await _mediator.Send(new AddTransactionRecordCommand { File = file });
        return Ok();
    }

    /// <summary>
    /// Get CSV file with transactions occured in clients time zone by year or month.
    /// </summary>
    /// <response code="200">Returns file with filtered transactions if any exists.</response>
    [HttpGet("inClientsTimeZone/{year}/{month}")]
    public async Task<IActionResult> GetTransactionsOccuredInClientsTimeZone(int year, int month = default)
    {
        var result =
            await _mediator.Send(new GetTransactionsOccuredInClientsTimeZoneQuery { Year = year, Month = month });

        string fileName;

        if (month == default)
            fileName = $"Transactions occured in clients timezone {year}.csv";
        else
            fileName = $"Transactions occured in clients timezone {month}/{year}.csv";

        return File(result, "text/csv", fileName);
    }

    /// <summary>
    /// Get CSV file with transactions occured in your time zone  by year or month.
    /// </summary>
    /// <response code="200">Returns file with filtered transactions if any exists.</response>
    /// /// <response code="400">If occurs error during getting your location coordinates.</response>
    [HttpGet("inUsersTimeZone/{year}/{month}")]
    public async Task<IActionResult> GetTransactionsOccuredInUsersTimeZoneQuery(int year, int month = default)
    {
        var result =
            await _mediator.Send(new GetTransactionsOccuredInUsersTimeZoneQuery() { Year = year, Month = month });

        string fileName;

        if (month == default)
            fileName = $"Transactions occured in your timezone {year}.csv";
        else
            fileName = $"Transactions occured in your timezone {month}/{year}.csv";

        return File(result, "text/csv", fileName);
    }
}