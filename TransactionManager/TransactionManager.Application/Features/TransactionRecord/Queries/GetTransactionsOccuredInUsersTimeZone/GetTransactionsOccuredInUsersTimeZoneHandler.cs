using MediatR;
using TransactionManager.Application.Interfaces;
using TransactionManager.Application.Services.CsvHelperService.Mapping;

namespace TransactionManager.Application.Features.TransactionRecord.Queries.GetTransactionsOccuredInUsersTimeZone;

public class GetTransactionsOccuredInUsersTimeZoneHandler :
    IRequestHandler<GetTransactionsOccuredInUsersTimeZoneQuery, byte[]>
{
    private readonly ICsvHelperService _csvHelperService;
    private readonly ITransactionRecordsService _transactionRecordsService;

    public GetTransactionsOccuredInUsersTimeZoneHandler(ICsvHelperService csvHelperService,
        ITransactionRecordsService transactionRecordsService)
    {
        _csvHelperService = csvHelperService;
        _transactionRecordsService = transactionRecordsService;
    }

    public async Task<byte[]> Handle(GetTransactionsOccuredInUsersTimeZoneQuery request,
        CancellationToken cancellationToken)
    {
        var transactionsRecords =
            await _transactionRecordsService.GetTransactionsOccuredInUsersTimeZone(request.Year, request.Month);

        return await _csvHelperService.WriteToCsvAsync(transactionsRecords, new TransactionInUsersTimeZoneWriteMap());
    }
}