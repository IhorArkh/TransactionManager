using MediatR;
using TransactionManager.Application.Interfaces;

namespace TransactionManager.Application.TransactionRecord.Queries.GetTransactionRecordsInClientLocalTime;

public class
    GetTransactionRecordsInClientLocalTimeHandler : IRequestHandler<GetTransactionRecordsInClientLocalTimeQuery, byte[]>
{
    private readonly ICsvHelperService _csvHelperService;
    private readonly ITransactionRecordsService _transactionRecordsService;

    public GetTransactionRecordsInClientLocalTimeHandler(ICsvHelperService csvHelperService,
        ITransactionRecordsService transactionRecordsService)
    {
        _csvHelperService = csvHelperService;
        _transactionRecordsService = transactionRecordsService;
    }

    public async Task<byte[]> Handle(GetTransactionRecordsInClientLocalTimeQuery request,
        CancellationToken cancellationToken)
    {
        var transactionsRecords = await _transactionRecordsService
            .GetTransactionRecordsInClientLocalTime(request.Year, request.Month);

        return _csvHelperService.WriteToCsv(transactionsRecords);
    }
}