using MediatR;

namespace TransactionManager.Application.TransactionRecord.Queries.GetTransactionRecordsInClientLocalTime;

public class GetTransactionRecordsInClientLocalTimeQuery : IRequest<byte[]>
{
    public int Year { get; set; }
    public int Month { get; set; }
}