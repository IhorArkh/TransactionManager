﻿using MediatR;
using TransactionManager.Application.Interfaces;
using TransactionManager.Application.Services.CsvHelperService.Mapping;

namespace TransactionManager.Application.Features.TransactionRecord.Queries.GetTransactionsOccuredInClientsTimeZone;

public class GetTransactionsOccuredInClientsTimeZoneHandler :
    IRequestHandler<GetTransactionsOccuredInClientsTimeZoneQuery, byte[]>
{
    private readonly ICsvHelperService _csvHelperService;
    private readonly ITransactionRecordsService _transactionRecordsService;

    public GetTransactionsOccuredInClientsTimeZoneHandler(ICsvHelperService csvHelperService,
        ITransactionRecordsService transactionRecordsService)
    {
        _csvHelperService = csvHelperService;
        _transactionRecordsService = transactionRecordsService;
    }

    public async Task<byte[]> Handle(GetTransactionsOccuredInClientsTimeZoneQuery request,
        CancellationToken cancellationToken)
    {
        var transactionsRecords = await _transactionRecordsService
            .GetTransactionsOccuredInClientsTimeZone(request.Year, request.Month);

        return await _csvHelperService.WriteToCsvAsync(transactionsRecords, new TransactionInClientsTimeZoneWriteMap());
    }
}