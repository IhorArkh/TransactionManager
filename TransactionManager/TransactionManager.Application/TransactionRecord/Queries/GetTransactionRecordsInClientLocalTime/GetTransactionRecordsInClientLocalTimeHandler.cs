using MediatR;
using TransactionManager.Application.Interfaces;

namespace TransactionManager.Application.TransactionRecord.Queries.GetTransactionRecordsInClientLocalTime;

public class
    GetTransactionRecordsInClientLocalTimeHandler : IRequestHandler<GetTransactionRecordsInClientLocalTimeQuery, byte[]>
{
    private readonly ICsvHelperService _csvHelperService;
    private readonly ITransactionRecordsTimeZoneService _transactionRecordsTimeZoneService;

    public GetTransactionRecordsInClientLocalTimeHandler(ICsvHelperService csvHelperService,
        ITransactionRecordsTimeZoneService transactionRecordsTimeZoneService)
    {
        _csvHelperService = csvHelperService;
        _transactionRecordsTimeZoneService = transactionRecordsTimeZoneService;
    }

    public async Task<byte[]> Handle(GetTransactionRecordsInClientLocalTimeQuery request,
        CancellationToken cancellationToken)
    {
        var transactionsRecords = await _transactionRecordsTimeZoneService
            .GetTransactionRecordsInClientLocalTime(request.Year);

        return _csvHelperService.WriteToCsv(transactionsRecords);
    }
}