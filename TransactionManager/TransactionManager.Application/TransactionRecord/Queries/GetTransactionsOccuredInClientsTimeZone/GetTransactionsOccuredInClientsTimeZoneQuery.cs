using MediatR;

namespace TransactionManager.Application.TransactionRecord.Queries.GetTransactionRecordsInClientLocalTime;

public class GetTransactionsOccuredInClientsTimeZoneQuery : IRequest<byte[]>
{
    public int Year { get; set; }
    public int Month { get; set; }
}