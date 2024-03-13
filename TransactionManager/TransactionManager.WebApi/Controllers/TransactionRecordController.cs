﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionManager.Application.Interfaces;
using TransactionManager.Application.TransactionRecord.Commands.AddTransactionRecord;
using TransactionManager.Application.TransactionRecord.Queries.GetTransactionRecordsInClientLocalTime;

namespace TransactionManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionRecordController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionRecordController(ICsvHelperService csvHelperService, IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddTransactionRecords(IFormFile file)
    {
        await _mediator.Send(new AddTransactionRecordCommand { File = file });
        return Ok();
    }

    [HttpGet("inClientTime/{year}")]
    public async Task<IActionResult> GetTransactionRecordsInClientsTimeByYear(int year)
    {
        var result = await _mediator.Send(new GetTransactionRecordsInClientLocalTimeQuery { Year = year });

        return File(result, "text/csv", "transactionRecords.csv");
    }
}