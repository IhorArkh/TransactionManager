using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionManager.Application.TransactionRecord.Commands.AddTransactionRecord;
using TransactionManager.Application.TransactionRecord.Queries.GetTransactionRecordsInClientLocalTime;
using TransactionManager.Application.TransactionRecord.Queries.GetTransactionsOccuredInUsersTimeZone;

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

    [HttpPost]
    public async Task<IActionResult> AddTransactions(IFormFile file)
    {
        await _mediator.Send(new AddTransactionRecordCommand { File = file });
        return Ok();
    }

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